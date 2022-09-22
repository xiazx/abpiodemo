using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.Demo.Shared.Utils
{
    public static class TaskExtensions
    {
        private static readonly ConcurrentDictionary<(Type, Type), Func<Task, object>> _resultDelegate =
            new ConcurrentDictionary<(Type, Type), Func<Task, object>>();

        /// <summary>
        /// 尝试从 <see cref="Task{T}"/> 获取返回值
        /// </summary>
        public static bool TryGetResult<T>(this Task task, out T result)
        {
            var type = task.GetType();
            var genType = type.GetGenericArguments(typeof(Task<>)).FirstOrDefault();
            var resultType = typeof(T);

            if (genType != null && resultType.IsAssignableFrom(genType))
            {
                var resultDelegate = _resultDelegate.GetOrAdd((type, resultType), t =>
                {
                    var p = Expression.Parameter(typeof(Task));
                    var body = Expression.Convert(
                        Expression.Property(
                            Expression.Convert(p, type), "Result"),
                        resultType
                    );
                    return Expression.Lambda<Func<Task, T>>(body, p).Compile();
                });

                result = (T)resultDelegate(task);
            }
            else
            {
                result = default;
            }

            return false;
        }

        /// <summary>
        /// 尝试从 <see cref="Task{T}"/> 获取返回值
        /// </summary>
        public static bool TryGetResult(this Task task, out object result)
        {
            return task.TryGetResult<object>(out result);
        }


        public static bool IsAsyncMethod(this MethodInfo method)
        {
            return method.ReturnType.IsTaskOrTaskOfT() || method.ReturnType.IsValueTaskOrValueTaskOfT();
        }

        public static bool IsTaskOrTaskOfT(this Type type)
        {
            if (type == typeof(Task))
                return true;
            if (type.GetTypeInfo().IsGenericType)
                return type.GetGenericTypeDefinition() == typeof(Task<>);
            return false;
        }

        public static bool IsValueTaskOrValueTaskOfT(this Type type)
        {
            if (type == typeof(ValueTask))
                return true;
            if (type.GetTypeInfo().IsGenericType)
                return type.GetGenericTypeDefinition() == typeof(ValueTask<>);
            return false;
        }
    }
}
