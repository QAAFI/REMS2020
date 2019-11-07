using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace REMS
{
    using Context;
    public class REMSDatabase : IREMSDatabase, IDisposable
    {
        #region IREMSDatabase implementation
        public bool IsOpen { get; private set; } = false;

        public REMSContext context = null;

        public IEnumerable<IEntity> Entities => context.Entities;

        public IEnumerable<string> Tables
        {
            get
            {
                if (context == null) return null;

                return context.Model.GetEntityTypes().Select(e => e.GetTableName());               
            }
        }

        public REMSDatabase()
        { }

        /// <summary>
        /// Returns a binding list with which to view the source data
        /// </summary>
        public DataTable this[string name]
        {
            get
            {
                string text = $"SELECT * FROM {name}";

                using var connection = new SqliteConnection(context.ConnectionString);
                connection.Open();
                using var command = new SqliteCommand(text, connection);
                using var reader = command.ExecuteReader();

                DataTable table = new DataTable(name);

                table.BeginLoadData();
                table.Load(reader);
                table.EndLoadData();

                return table;
            }
        }

        //public BindingList<IEntity> this[string name]
        //{
        //    get
        //    {
        //        IEnumerable<IEntity> values = (IEnumerable<IEntity>)context.GetType().GetProperty(name).GetValue(context);
        //        BindingList<IEntity> list = new BindingList<IEntity>(values.ToList());

        //        return list;
        //    }
        //}


        public void Create(string file)
        {
            context = new REMSContext(file);
            context.Database.EnsureCreated();
            context.SaveChanges();
        }

        public void Open(string file)
        {        
            if (IsOpen) Close();

            context = new REMSContext(file);
            IsOpen = true;            
        }        

        public void ImportData(string file)
        {
            using var excel = ExcelImporter.ReadRawData(file);
            using var connection = new SqliteConnection(context.ConnectionString);
            connection.Open();

            foreach (DataTable table in excel.Tables)
            {
                NewImportTable(table);
            } 
        }

        private void NewImportTable(DataTable table)
        {
            var values = table.Rows.Cast<DataRow>().Select(r => r.ItemArray);
            var names = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

            var entity = context.Entities.Where(e => e.CheckName(table.TableName)).First();            
            var entities =  values.Select(v => entity.Create(v, names));

            context.AddRange(entities);
            context.SaveChanges();
        }

        public void ExportData(string file)
        {
            //using Simulations simulations = new Simulations();            

            //var replacements = new Replacements() { Name = "Replacements" };
            //replacements.Add(ApsimNode.ReadFromFile<Plant>("Sorghum.json"));

            //simulations.Add(new DataStore());
            //simulations.Add(replacements);
            //simulations.Add(GetValidations());
            //simulations.WriteToFile(file);

            //GenerateMets(Path.GetDirectoryName(file));
        }

        private void GenerateMets(string path)
        {
            var mets = from met in context.MetStations
                       select met;

            foreach (var met in mets)
            {
                string file = path + "\\" + met.Name + ".met";

                using var stream = new FileStream(file, FileMode.Create);
                using var writer = new StreamWriter(stream);
                
                writer.WriteLine($"latitude = {met.Latitude}");
                writer.WriteLine($"longitude = {met.Longitude}");
                writer.WriteLine($"tav = {met.TemperatureAverage}");
                writer.WriteLine($"amp = {met.Amp}\n");

                writer.WriteLine($"{"Year", -8}{"Day", -8}{"maxt", -8}{"mint", -8}{"radn", -8}{"Rain", -8}");
                writer.WriteLine($"{" () ", -8}{" () ",-8}{" () ", -8}{" () ", -8}{" () ", -8}{" () ", -8}");

                var dates = context.MetDatas
                    .Select(d => d.Date)
                    .Distinct()
                    .OrderBy(d => d.Date);

                var TMAX = context.Traits.First(t => t.Name == "TMAX");
                var TMIN = context.Traits.First(t => t.Name == "TMIN");
                var SOLAR = context.Traits.First(t => t.Name == "SOLAR");
                var RAIN = context.Traits.First(t => t.Name == "RAIN");

                foreach (var date in dates)
                {
                    var value = from data in context.MetDatas
                                where data.Date == date
                                where data.Value.HasValue
                                select data;

                    double tmax = Math.Round(value.FirstOrDefault(d => d.Trait == TMAX).Value.Value, 2);
                    double tmin = Math.Round(value.FirstOrDefault(d => d.Trait == TMIN).Value.Value, 2);
                    double radn = Math.Round(value.FirstOrDefault(d => d.Trait == SOLAR).Value.Value, 2);
                    double rain = Math.Round(value.FirstOrDefault(d => d.Trait == RAIN).Value.Value, 2);

                    writer.Write($"{date.Year, -8}{date.Day, -8}{tmax, -8}{tmin,-8}{radn,-8}{rain,-8}\n");
                }
            }
            
        }

        /// <summary>
        /// Saves the database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public void Close()
        {
            Save();
            context.Database.CloseConnection();

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
            }
        }
        #endregion
    }
}
