using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context
{
    public abstract class BaseEntity : IEntity
    {
        protected readonly List<PropertyInfo> properties;

        public BaseEntity()
        {
            properties = GetType().GetProperties().ToList();          
        }

        public abstract void BuildModel(ModelBuilder modelBuilder);

        public IEntity Create(object[] values, string[] names)
        {
            IEntity clone = Activator.CreateInstance(GetType()) as IEntity;

            for (int i = 0; i < values.Length; i++)
            {                
                var property = properties.FirstOrDefault(p => p.Name == names[i]);
                if (property == null) continue;
                var value = ParseValue(values[i], property.PropertyType);
                property.SetValue(clone, value);
            }
            
            return clone;
        }

        private object ParseValue(object value, Type type)
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

        public bool CheckName(string name)
        {
            if (GetType().Name + "s" == name) 
                return true;
            else 
                return false;
        }

    }
}
