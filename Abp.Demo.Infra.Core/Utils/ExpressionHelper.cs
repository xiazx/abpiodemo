using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq.Expressions;

namespace Abp.Demo.Infra.Core.Utils
{
    public class ExpressionHelper
    {
        #region Methods
        /// <summary>
        /// </summary>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> express)
        {
            var memberExpress = express.Body as MemberExpression;
            if (memberExpress == null)
            {
                throw new ArgumentException("Not is MemberExpression", nameof(express));
            }

            return memberExpress.Member.Name;
        }

        /// <summary>
        /// </summary>
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> express)
        {
            var memberExpress = express.Body as MemberExpression;
            if (memberExpress == null)
            {
                throw new ArgumentException("Not is MemberExpression", nameof(express));
            }

            return memberExpress.Member.Name;
        }

        /// <summary>
        /// </summary>
        public static TProperty GetPropertyValue<T, TProperty>(T obj, Expression<Func<T, TProperty>> express)
        {
            return (TProperty)express.GetPropertyAccess().GetValue(obj);
        }

        public static string GetPropertyFullName<TProperty>(Expression<Func<TProperty>> express)
        {
            var memberExpress = express.Body as MemberExpression;
            if (memberExpress == null)
            {
                throw new ArgumentException("Not is MemberExpression", nameof(express));
            }


            return GetClassName(memberExpress.Member.ReflectedType) + "." + GetPropertyName(express);
        }

        public static string GetPropertyFullName<T, TProperty>(Expression<Func<T, TProperty>> express)
        {
            return GetClassName(typeof(T)) + "." + GetPropertyName(express);
        }

        public static string GetClassName(Type type)
        {
            return type.Name;
        }
        #endregion
    }
}
