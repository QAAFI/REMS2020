using Rems.Domain.Entities;
using System.Collections.Generic;


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
    }
}
