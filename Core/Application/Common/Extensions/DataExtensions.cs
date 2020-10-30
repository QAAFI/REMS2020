using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Rems.Application.Common.Extensions
{
    public static class DataExtensions
    {
        public static void RemoveDuplicateRows(this DataTable table, IEqualityComparer<DataRow> comparer = null)
        {
            if (comparer == null) 
                comparer = new DataRowItemComparer();

            var rows = table.Rows.Cast<DataRow>()
                .Distinct(comparer)
                .Select(r => r.ItemArray)
                .ToArray();

            table.Rows.Clear();
            foreach (var row in rows) table.Rows.Add(row);
        }

        public static IEntity ToEntity(this DataRow row, IRemsDbContext context, Type type, PropertyInfo[] infos)
        {
            IEntity entity = Activator.CreateInstance(type) as IEntity;

            foreach (var info in infos)
            {
                var value = row[info.Name];
                if (value is DBNull) continue; // Use default value if DBNull

                var itype = info.PropertyType;

                // Is the property an entity?
                if (typeof(IEntity).IsAssignableFrom(itype)) 
                {
                    // Does the entity already exist?
                    if (context.FindMatchingEntity(itype, value) is IEntity e) 
                    {
                        info.SetValue(entity, e);
                        continue;
                    }

                    // If the entity was not found, assume that it was referred
                    // to by name, and create a new entity using the given value
                    INamed named = Activator.CreateInstance(itype) as INamed;
                    named.Name = value.ToString();

                    info.SetValue(entity, named);

                    context.Attach(named);
                    context.SaveChanges();
                }
                else
                {
                    entity.SetValue(info, value);
                }
            }

            return entity;
        }

        public static PropertyInfo FindProperty(this DataColumn col, Type type)
        {
            var name = col.ColumnName;
            var props = type.GetProperties();

            // Look for the property by name
            if (props.SingleOrDefault(p => p.Name == name) is PropertyInfo i) return i;

            // Look for the property by coarse string matching
            var infos = props.Where(p => p.Name.Contains(name) || name.Contains(p.Name));
            if (infos.Count() <= 1) return infos.SingleOrDefault();

            // If a property is not found
            var validater = EventManager.InvokeItemNotFound(name);

            if (!validater.IsValid)
                return null;
            else
                return props.First(p => p.Name == validater.Item);
        }

        public static PropertyInfo FindInfo(this DataColumn col, Type type)
        {
            // Search for a property matching the column name
            if (type.GetProperty(col.ColumnName) is PropertyInfo i)
            {
                return i;
            }
            // Test if the column name matches the entity name
            else if (col.ColumnName == type.Name)
            {
                // Guess that there is a column/property called Name.
                // If not, defer back to user selection
                col.ColumnName = "Name";
                return col.FindInfo(type);
            }
            else
            {
                //var ptions = type.GetProperties()
                //    .Where(p => !(p.PropertyType is ICollection))
                //    .Select(e => e.Name)
                //    .Where(n => !n.Contains("Id"))
                //    .ToList();

                var validater = EventManager.InvokeItemNotFound(col.ColumnName);

                if (!validater.IsValid) return null;

                col.ColumnName = validater.Item;

                // TODO: Rather than use recursion, this method can probably be separated into smaller methods
                return col.FindInfo(type);
            }
        }
    }

    public class DataRowItemComparer : IEqualityComparer<DataRow>
    {
        public bool Equals(DataRow x, DataRow y)
        {
            var xItems = x.ItemArray;
            var yItems = y.ItemArray;

            if (xItems.Length != yItems.Length) return false;

            for (int i = 0; i < xItems.Length; i++)
            {
                if (!Equals(xItems[i], yItems[i])) return false;
            }

            return true;
        }

        public int GetHashCode(DataRow obj)
        {
            unchecked
            {
                if (obj.ItemArray == null) return 0;

                int hash = 17;

                foreach (var o in obj.ItemArray)
                {
                    hash *= 31;
                    hash += o.GetHashCode();
                }
                return hash;
            }
        }
    }
}
