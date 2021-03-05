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
                if (info.GetValue(entity)?.ToString().ToLower() == value.ToString().ToLower()) 
                    return true;
            return false;
        }
    }
}
