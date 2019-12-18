using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common
{
    public static partial class Functions
    {
        /// <summary>
        /// Converts an object to a new type, allowing for Nullable and DBNull objects.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static object ConvertNullableObject(object value, Type type)
        {
            var t = value.GetType();
            var u = Nullable.GetUnderlyingType(type);

            // If the source data is null
            if (t == typeof(DBNull))
                return null;

            // If the property itself is nullable
            if (u != null)
            {
                if (u != t)
                    return Convert.ChangeType(value, u);
                else
                    return value;
            }

            // Default conversion for non-nullable types/values
            if (t != type)
                return Convert.ChangeType(value, type);
            else
                return value;
        }
    }
}
