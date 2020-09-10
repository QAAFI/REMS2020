using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Extensions
{
    public static class ObjectExtensions
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
    }
}
