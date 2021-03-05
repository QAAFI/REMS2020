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
        public static void FindExperiments(this DataSet data)
        {
            if (!(data.Tables["Experiments"] is DataTable table))
                return;

            var es = table.Rows
                .Cast<DataRow>()
                .ToDictionary
                (
                    r => Convert.ToInt32(r[0]),
                    r => r[1].ToString()
                );

            data.ExtendedProperties["Experiments"] = es;
        }

        public static void RemoveDuplicateRows(this DataTable table, IEqualityComparer<DataRow> comparer = null)
        {            
            comparer = comparer ?? new DataRowItemComparer();

            var rows = table.Rows.Cast<DataRow>()
                .Distinct(comparer)
                .Select(r => r.ItemArray)
                .ToArray();

            table.Rows.Clear();
            foreach (var row in rows) table.Rows.Add(row);
        }

        /// <summary>
        /// Replace an ExperimentId column with an Experiment column,
        /// referencing the experiments which belong to the DataSet the table is in.
        /// </summary>
        /// <param name="table"></param>
        public static void ConvertExperiments(this DataTable table)
        {
            // There needs to be an IDs column to convert
            if (!(table.Columns["ExperimentId"] is DataColumn ids))
                return;

            // An experiments table is handled seperately
            if (table.TableName == "Experiments")
            {
                table.Columns.Remove(ids);
                return;
            }

            if (!(table.DataSet.ExtendedProperties["Experiments"] is Dictionary<int, string> keys))
                return;

            int ord = ids.Ordinal;

            var exps = new DataColumn("Experiment", typeof(string));
            table.Columns.Add(exps);
            exps.SetOrdinal(ord);

            foreach (DataRow row in table.Rows)
                row[exps] = keys[Convert.ToInt32(row[ids])];

            table.Columns.Remove(ids);
        }

        public static IEntity ToEntity(this DataRow row, IRemsDbContext context, Type type, PropertyInfo[] infos)
        {
            IEntity entity = Activator.CreateInstance(type) as IEntity;

            foreach (var info in infos)
            {
                object value = row[info.Name];

                // Use default value if cell is empty
                if (value is DBNull || value is "") continue;

                var itype = info.PropertyType;
                                
                // If the property is an entity, find or create the matching entity
                if (typeof(IEntity).IsAssignableFrom(itype))
                    value = context.FindMatchingEntity(itype, value) ?? context.CreateEntity(itype, value);                
                else
                    // If the value is nullable, convert its type
                    value = Convert.ChangeType(value, Nullable.GetUnderlyingType(itype) ?? itype);
                
                info.SetValue(entity, value);
            }

            return entity;
        }
        
        public static IEnumerable<PropertyInfo> GetUnmappedProperties(this DataColumn col)
        {
            // Find all the infos which are already mapped to a column
            var infos = col.Table.Columns.Cast<DataColumn>()
                .Select(c => c.ExtendedProperties["Info"])
                .Where(o => o != null)
                .Cast<PropertyInfo>();

            bool notEnum(PropertyInfo info)
            {
                // TODO: Find a better way to test this
                if (info.PropertyType.Name == typeof(ICollection<>).Name)
                    return false;

                return true;
            }

            // Find all the non-mapped infos
            var type = col.Table.ExtendedProperties["Type"] as Type;
            var properties = type.GetProperties()
                .Except(infos)
                .Where(p => notEnum(p));

            return properties;
        }

        /// <summary>
        /// A bunch of ugly string matching to find a property because unfiltered excel data
        /// </summary>
        public static PropertyInfo FindProperty(this DataColumn col)
        {
            // Be careful when changing the order of these tests - they look independent are not.
            // Some of the later tests rely on assumptions that are excluded in the initial tests.

            var type = col.Table.ExtendedProperties["Type"] as Type;
            col.ExtendedProperties["Valid"] = true;

            // Trim whitespace
            col.ColumnName = col.ColumnName.Replace(" ", "");

            // Test for a direct match
            if (type.GetProperty(col.ColumnName) is PropertyInfo x)
                return x;

            // Test if the column is called name
            if (col.ColumnName == type.Name && type.GetProperty("Name") is PropertyInfo y)
            {
                col.ColumnName = "Name";
                return y;
            }

            // Trim the column and look for direct match again
            var name = col.ColumnName.Replace("Name", "").Replace("name", "").Trim();
            if (type.GetProperty(name) is PropertyInfo z)
            {
                col.ColumnName = name;
                return z;
            }

            // Assume its called name
            if (name == type.Name)
            {
                col.ColumnName = "Name";
                return type.GetProperty("Name");
            }

            // Assume the column has the type name attached
            var prop = col.ColumnName.Replace(type.Name, "").Trim();
            if (type.GetProperty(prop) is PropertyInfo i)
            {
                col.ColumnName = prop;
                return i;
            }

            // Check if there is a single umapped property which contains the column name
            var s = col.GetUnmappedProperties()
                .Where(p => col.ColumnName.ToLower().Contains(p.Name.ToLower()));

            if (s.Count() == 1)
            {
                col.ColumnName = s.Single().Name;
                return s.Single();
            }

            // If no property was found
            col.ExtendedProperties["Valid"] = false;
            return null;
        }

        private static readonly Dictionary<string, string> map = new Dictionary<string, string>()
        {
            {"ExpID", "ExperimentId" },
            {"ExpId", "ExperimentId" },
            {"N%", "Nitrogen" },
            {"P%", "Phosphorus" },
            {"K%", "Potassium" },
            {"Ca%", "Calcium" },
            {"S%", "Sulfur" },
            {"Other%", "OtherPercent" },
            {"amp", "Amplitude" },
            {"tav", "TemperatureAverage" },
            {"PlotID", "Plot" }
        };

        public static void ReplaceName(this DataColumn col)
        {
            if (map.ContainsKey(col.ColumnName))
                col.ColumnName = map[col.ColumnName];
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
