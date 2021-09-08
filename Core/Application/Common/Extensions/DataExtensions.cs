﻿using Rems.Application.Common.Interfaces;
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
        /// <summary>
        /// Searches the dataset for an experiments table, and creates a mapping between the Name and Id columns
        /// </summary>
        /// <param name="data"></param>
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

        /// <summary>
        /// Purges rows with identical values from the dataset
        /// </summary>        
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

        public static void RemoveEmptyColumns(this DataTable table)
        {
            foreach (DataColumn col in table.Columns)
                if (col.ColumnName.Contains("Column"))
                    table.Columns.Remove(col);
        }

        /// <summary>
        /// Replace an ExperimentId column with an Experiment column,
        /// referencing the experiments which belong to the DataSet the table is in.
        /// </summary>
        /// <param name="table"></param>
        public static void ConvertExperiments(this DataTable table, string[] names)
        {
            var col = table.Columns.OfType<DataColumn>()
                .FirstOrDefault(c => names.Contains(c.ColumnName));

            // There needs to be an IDs column to convert
            if (col is not DataColumn ids)
                return;

            // An experiments table is handled seperately
            if (table.TableName == "Experiments")
            {
                table.Columns.Remove(ids);
                return;
            }

            // Cannot convert if the mapping does not exist
            if (!(table.DataSet.ExtendedProperties["Experiments"] is Dictionary<int, string> keys))
                return;

            // Replace the Id column with the names column
            int ord = ids.Ordinal;
            var exps = new DataColumn("Experiment", typeof(string));
            table.Columns.Add(exps);
            exps.SetOrdinal(ord);

            // Insert the values into the new column
            foreach (DataRow row in table.Rows)
                row[exps] = keys[Convert.ToInt32(row[ids])];

            // Remove the old column
            table.Columns.Remove(ids);
        }

        /// <summary>
        /// Converts a row from a <see cref="DataTable"/> into an <see cref="IEntity"/> 
        /// that can be attached to the database
        /// </summary>
        /// <param name="row">Source data</param>
        /// <param name="context">Context to attach result to</param>
        /// <param name="type">CLR <see cref="Type"/> of the <see cref="IEntity"/></param>
        /// <param name="infos">Properties which map to <see cref="DataColumn"/>s in the <see cref="DataRow"/></param>
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
        
        /// <summary>
        /// Find any entity properties belonging to the parent table that are not mapped to a column in the table
        /// </summary>
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
        /// Attempt to find an entity property that corresponds to the <see cref="DataColumn"/>
        /// </summary>
        public static PropertyInfo FindProperty(this DataColumn col)
        {
            // Be careful when changing the order of these tests - they look independent are not.
            // Some of the later tests rely on assumptions that are excluded in the initial tests.

            if (col.Table?.ExtendedProperties["Type"] is not Type type)
                return null;

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

        public static string GetText(this DataRow row, string column)
            => row[column].ToString();

        public static int GetInt32(this DataRow row, string column, bool required = true)
        {
            if (int.TryParse(row[column].ToString(), out int x))
                return x;

            if (!required)
                return default;

            throw new DataTypeException(column, row.Table.TableName, "Integer");
        }

        public static double GetDouble(this DataRow row, string column, bool required = true)
        {
            if (double.TryParse(row[column].ToString(), out double x))
                return x;

            if (!required)
                return default;

            throw new DataTypeException(column, row.Table.TableName, "Decimal");
        }

        public static DateTime GetDate(this DataRow row, string column, bool required = true)
        {
            if (DateTime.TryParse(row[column].ToString(), out DateTime x))
                return x;

            if (!required)
                return default;

            throw new DataTypeException(column, row.Table.TableName, "Date");
        }
    }

    /// <summary>
    /// Compare two <see cref="DataRow"/>s by the values of the items they contain
    /// </summary>
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
