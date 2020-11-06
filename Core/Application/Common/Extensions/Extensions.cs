using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common.Extensions
{
    public static class Extensions
    {
        public static object ConvertDBValue(this object value, Type type)
        {
            // Convert nullable numerics
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlying = Nullable.GetUnderlyingType(type);
                return Convert.ChangeType(value, underlying);
            }

            // Convert normal numerics
            return Convert.ChangeType(value, type);
        }

        public static T ConvertDBValue<T>(this object value)
        {
            Type type = typeof(T);

            // Convert nullable numerics
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlying = Nullable.GetUnderlyingType(type);
                return (T)Convert.ChangeType(value, underlying);
            }

            // Convert normal numerics
            return (T)Convert.ChangeType(value, type);
        }

        public static T SetParam<T>(this IParameterised query, object value)
        {
            if (value is T t)
                return t;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(T)} \n Received: {value.GetType()}");
        }

        public async static Task<T> Send<T>(this IRequest<T> query, QueryHandler handler)
        {
            return (T) await handler(query);
        }
    }
}
