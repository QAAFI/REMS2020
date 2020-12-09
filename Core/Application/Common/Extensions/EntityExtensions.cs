using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Rems.Application.Common.Extensions
{
    public static class EntityExtensions
    {
        public static bool HasValue(this IEntity entity, object value, PropertyInfo[] infos)
        {
            foreach (var info in infos)
                if (info.GetValue(entity)?.ToString() == value.ToString()) 
                    return true;
            return false;
        }

        public static void SetValue(this IEntity entity, PropertyInfo info, object value)
        {
            // Entity will use default values for null or 0 entries
            if (value is DBNull) return;
            if (value is double d && d == 0) return;

            var type = info.PropertyType;

            // Handle strings
            if (type == typeof(string))
            {
                info.SetValue(entity, value);
                return;
            }
            // Handle value types
            else
            {
                var v = value.ConvertDBValue(type);
                info.SetValue(entity, v);
            }

            return;
        }
    }
}
