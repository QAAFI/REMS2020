using MediatR;
using Models;
using Rems.Application.Common.Interfaces;
using System;
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
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// Perform the given action for each element in the source
        /// </summary>
        /// <typeparam name="TCast">The type the element is cast to</typeparam>
        /// <typeparam name="TElement">The element type</typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        public static void ForEach<TCast, TElement>(this IEnumerable<TElement> source, Action<TCast> action) where TCast : TElement
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            foreach (TCast element in source)
                action(element);
        }

        /// <summary>
        /// Perform the given action for each element in the source, with indexing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">The indexed action</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            int index = -1;
            
            foreach (T element in source)
            {
                checked { index++; }
                action(element, index);
            }
        }        
    }
}
