using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Rems.Application.Common.Extensions
{
    public static class EntityExtensions
    {
        public static void Update(this IEntity entity, Dictionary<string, object> pairs)
        {
            foreach (var pair in pairs)
            {
                var property = entity.GetType().GetProperty(pair.Key);
                if (property == null) continue;
                var value = Functions.ConvertNullableObject(pair.Value, property.PropertyType);
                property.SetValue(entity, value);
            }
        }

        public static IEnumerable<PropertyInfo> GetPropertyByAttribute<TAttribute>(this IEntity entity) where TAttribute : Attribute
        {
            var props = entity.GetType().GetProperties();
            return props.Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(TAttribute)));
        }

        public static PropertyInfo GetPropertyByName(this IEntity entity, string name)
        {
            var props = entity.GetType().GetProperties();
            return props.First(p => p.Name == name);
        }

        public static bool HasValue(this IEntity entity, object value)
        {
            var infos = entity.GetType().GetProperties();

            foreach (var info in infos) if (info.GetValue(entity) == value) return true;
            return false;
        }

        public static bool HasValue(this IEntity entity, object value, PropertyInfo[] infos)
        {
            foreach (var info in infos)
                if (info.GetValue(entity)?.ToString() == value.ToString()) return true;
            return false;
        }
    }
}
