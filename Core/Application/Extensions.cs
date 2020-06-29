using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rems.Application
{
    public static class Extensions
    {
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
    }
}
