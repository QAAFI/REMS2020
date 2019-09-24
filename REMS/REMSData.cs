using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Models;

namespace REMS
{
    public class REMSDatabase : IREMSDatabase, IDisposable
    {
        #region IREMSDatabase implementation
        public bool IsOpen { get; private set; } = false;

        private REMSContext context = null;

        private SqliteConnection connection;

        private SqliteCommand command = new SqliteCommand();
        
        public List<string> Tables
        {
            get
            {
                var tables = new List<string>();

                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) tables.Add((string)reader["name"]);
                    tables.RemoveAt(tables.Count - 1);
                    return tables;
                }         
            }
        }

        public REMSDatabase()
        { }

        /// <summary>
        /// Access DataTable by name
        /// </summary>
        public DataTable this[string name]
        {
            get
            {
                command.CommandText = $"SELECT * FROM {name}";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    DataTable table = new DataTable(name);

                    table.BeginLoadData();
                    table.Load(reader);
                    table.EndLoadData();

                    return table;
                }
            }
        }

        public void Create(string file)
        {
            context = new REMSContext($"Data Source={file};");
            context.Database.EnsureCreated();
            context.SaveChanges();
        }

        public void Open(string file)
        {        
            if (IsOpen) Close();            

            connection = new SqliteConnection($"Data Source={file};");
            connection.Open();

            command.Connection = connection;

            IsOpen = true;            
        }

        private object ParseItem(object item)
        {
            if (item.GetType() == typeof(DBNull)) return "null";

            return item;
        }

        public void ImportData(string file)
        {
            var excel = ExcelImporter.ReadRawData(file);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    command.Transaction = transaction;
                    foreach (DataTable table in excel.Tables)
                    {
                        List<string> names = new List<string>();
                        foreach (DataColumn column in table.Columns) names.Add(column.ColumnName);
                        string columns = String.Join(", ", names);

                        string insert = $"INSERT INTO {table.TableName} ({columns})";

                        foreach (DataRow row in table.Rows)
                        {
                            var values = row.ItemArray.Select(i => ParseItem(i));
                            //var values = String.Join(", ", row.ItemArray);                        
                            command.CommandText = $"{insert} VALUES ({String.Join(", ", values)});";
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                finally
                {
                    command.Transaction = null;
                }                
            }
        }

        public void ExportData(string file)
        {
            ExampleApsimX.GenerateExampleA(file);
        }
        

        /// <summary>
        /// Saves the database
        /// </summary>
        public void Save()
        {
            
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public void Close()
        {
            Save();
            connection.Close();

            IsOpen = false;
        }
        #endregion

        #region IDisposable implemenation

        private bool disposed = false;

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                context.Dispose();
                connection.Dispose();
                command.Dispose();      
            }
        }
        #endregion
    }
}
