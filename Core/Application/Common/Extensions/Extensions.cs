using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Rems.Application.Common.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Cast an object to the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to cast</param>
        /// <exception cref="Exception">Thrown when the object is not of the expected type</exception>
        public static T CastParam<T>(this IParameterised query, object value)
        {
            if (value is T t)
                return t;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(T)} \n Received: {value.GetType()}");
        }

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
