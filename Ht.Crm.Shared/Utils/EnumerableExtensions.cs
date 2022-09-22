using System;
using System.Collections.Generic;
using System.Linq;

namespace Abp.Demo.Shared.Utils
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 遍历元素
        /// </summary>
        public static IEnumerable<TElement> Each<TElement>(this IEnumerable<TElement> source,
            Action<TElement> itemAction)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (itemAction == null)
                throw new ArgumentNullException(nameof(itemAction));

            IEnumerable<TElement> Iterator(IEnumerable<TElement> source1, Action<TElement> itemAction1)
            {
                foreach (var item in source1)
                {
                    itemAction1(item);
                    yield return item;
                }
            }

            return Iterator(source, itemAction);

        }

        /// <summary>
        /// 添加多个
        /// </summary>
        public static T AddRange<T, TElement>(this T source, IEnumerable<TElement> data)
            where T : ISet<TElement>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (data != null)
            {
                foreach (var item in data)
                {
                    source.Add(item);
                }
            }

            return source;
        }

        ///// <summary>
        ///// Concatenates the members of a constructed <see cref="IEnumerable{T}"/> collection of type System.String, using the specified separator between each member.
        ///// This is a shortcut for string.Join(...)
        ///// </summary>
        ///// <param name="source">A collection that contains the strings to concatenate.</param>
        ///// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element.</param>
        ///// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.</returns>
        //public static string JoinAsString(this IEnumerable<string> source, string separator)
        //{
        //    return string.Join(separator, source);
        //}

        ///// <summary>
        ///// Concatenates the members of a collection, using the specified separator between each member.
        ///// This is a shortcut for string.Join(...)
        ///// </summary>
        ///// <param name="source">A collection that contains the objects to concatenate.</param>
        ///// <param name="separator">The string to use as a separator. separator is included in the returned string only if values has more than one element.</param>
        ///// <typeparam name="T">The type of the members of values.</typeparam>
        ///// <returns>A string that consists of the members of values delimited by the separator string. If values has no members, the method returns System.String.Empty.</returns>
        //public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        //{
        //    return string.Join(separator, source);
        //}

        ///// <summary>
        ///// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        ///// </summary>
        ///// <param name="source">Enumerable to apply filtering</param>
        ///// <param name="condition">A boolean value</param>
        ///// <param name="predicate">Predicate to filter the enumerable</param>
        ///// <returns>Filtered or not filtered enumerable based on <paramref name="condition"/></returns>
        //public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        //{
        //    return condition
        //        ? source.Where(predicate)
        //        : source;
        //}

        ///// <summary>
        ///// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        ///// </summary>
        ///// <param name="source">Enumerable to apply filtering</param>
        ///// <param name="condition">A boolean value</param>
        ///// <param name="predicate">Predicate to filter the enumerable</param>
        ///// <returns>Filtered or not filtered enumerable based on <paramref name="condition"/></returns>
        //public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        //{
        //    return condition
        //        ? source.Where(predicate)
        //        : source;
        //}

    }
}