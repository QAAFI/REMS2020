using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rems.Domain.Attributes;

namespace Rems.Application.Common.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Perform the given action for each element in the source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext() == true)
            {
                T current = (T)enumerator.Current;
                action(current);
                yield return current;
            }
        }

        /// <summary>
        /// Perform the given action for each element in the source
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static IEnumerable<TElement> ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            foreach (TElement element in source)
                action(element);

            return source;
        }

        /// <summary>
        /// Perform the given action for each element in the source
        /// </summary>
        /// <typeparam name="TCast">The type the element is cast to</typeparam>
        /// <typeparam name="TElement">The element type</typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static IEnumerable<TElement> ForEach<TCast, TElement>(this IEnumerable<TElement> source, Action<TCast> action) where TCast : TElement
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            foreach (TCast element in source)
                action(element);

            return source;
        }

        /// <summary>
        /// Perform the given action for each element in the source, with indexing
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">The indexed action</param>
        public static IEnumerable<TElement> ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement, int> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            int index = -1;
            
            foreach (TElement element in source)
            {
                checked { index++; }
                action(element, index);
            }

            return source;
        }        

        /// <summary>
        /// Tests two sequences for equivalence (each element has a 1:1 mapping)
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool SequenceEquivalent<TElement>(this IEnumerable<TElement> source, IEnumerable<TElement> other)
            => source.OrderBy(e => e).SequenceEqual(other.OrderBy(e => e));

        public static double[] Cumulative(this double[] values)
        {
            var result = new double[values.Length];
            result[0] = values[0];
            for (int i = 1; i < values.Length; i++)
                result[i] = values[i] + result[i - 1];

            return result;
        }

        public static IEnumerable<PropertyInfo> ExpectedProperties(this Type type)
            => type.GetProperties().Where(p => p.GetCustomAttribute<Expected>() is not null);

        /// <summary>
        /// Checks if the given name is one of the expected names for the property
        /// </summary>
        public static bool IsExpected(this PropertyInfo info, string name)
            => info.GetCustomAttribute<Expected>().Names.Contains(name);
    }
}
