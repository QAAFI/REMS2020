using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Rems.Application.Common.Extensions
{
    public static class Extensions
    {
        public static T SetParam<T>(this IParameterised query, object value)
        {
            if (value is T t)
                return t;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(T)} \n Received: {value.GetType()}");
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException(source.ToString());
            if (action == null) throw new ArgumentNullException(action.ToString());

            foreach (T element in source)
                action(element);
        }

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
