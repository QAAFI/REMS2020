using System;
using System.Collections;
using System.Collections.Generic;

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
    }
}
