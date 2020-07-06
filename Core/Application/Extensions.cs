﻿using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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

        public static IForeignKey FindForeignKey(this IEntityType entity, string name)
        {
            return entity.GetForeignKeys().FirstOrDefault(k => k.GetName() == name);
        }

        public static string GetName(this IForeignKey key)
        {
            return key.Properties.First().Name;
        }
    }
}
