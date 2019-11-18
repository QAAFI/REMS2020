using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Rems.Domain;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;


namespace Rems.Application.Common.Extensions
{
    public static class EEntity
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
    }
}
