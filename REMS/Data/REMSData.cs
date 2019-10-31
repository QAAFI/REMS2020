using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Models;
using Models.Core;
using Models.Functions;
using Models.Functions.DemandFunctions;
using Models.Functions.SupplyFunctions;
using Models.PMF;
using Models.PMF.Library;
using Models.PMF.Organs;
using Models.PMF.Phen;
using Models.PMF.Struct;
using Models.PostSimulationTools;
using Models.Report;
using Models.Soils;
using Models.Storage;
using Models.Surface;

namespace REMS
{
    using Context;
    public class REMSDatabase : IREMSDatabase, IDisposable
    {
        #region IREMSDatabase implementation
        public bool IsOpen { get; private set; } = false;

        private REMSContext context = null;

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
                //ImportTable(table, connection);
                NewImportTable(table);
            }
        }

        private void ImportTable(DataTable table, SqliteConnection connection)
        {            
            using var transaction = connection.BeginTransaction();
            using var command = new SqliteCommand()
            {
                Connection = connection,
                Transaction = transaction
            };

            List<string> names = new List<string>();

            foreach (DataColumn column in table.Columns)
            {
                names.Add(column.ColumnName);
                command.Parameters.Add(new SqliteParameter(column.ColumnName, null));
            }

            string fields = String.Join(", ", names);
            string values = String.Join(", @", names);
            command.CommandText = $"INSERT INTO {table.TableName} ({fields}) VALUES (@{values})";

            foreach (DataRow row in table.Rows)
            {
                UpdateParameters(command, row.ItemArray);  
                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        private void NewImportTable(DataTable table)
        {
            var values = table.Rows.Cast<DataRow>().Select(r => r.ItemArray);
            var names = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

            var entity = context.Entities.Where(e => e.CheckName(table.TableName)).First();            
            var entities =  values.Select(v => entity.Create(v, names));

            //SafeAdd(entities);
            context.AddRange(entities);
            context.SaveChanges();
        }

        private void SafeAdd(IEnumerable<IEntity> entities)
        {
            int count = 0;
            foreach (var entity in entities)
            {
                count++;
                try
                {
                    context.Add(entity);
                    context.SaveChanges();
                }
                catch
                {
                    var t = true;
                }
            }            
        }

        private void UpdateParameters(SqliteCommand command, object[] values)
        {
            for(int i = 0; i < values.Length; i++)
            {
                command.Parameters[i].Value = ParseItem(values[i]);
            }
        }

        private object ParseItem(object item)
        {
            if (item.GetType() == typeof(DBNull)) return "null";

            return item;
        }

        public void ExportData(string file)
        {
            Simulations simulations = new Simulations();            

            var replacements = new Replacements() { Name = "Replacements" };
            replacements.Add(ApsimNode.ReadFromFile<Plant>("Sorghum.json"));

            simulations.Add(new DataStore());
            simulations.Add(replacements);
            simulations.Add(GetSimulations());
            simulations.WriteToFile(file);
        }
        
        private IEnumerable<Simulation> GetSimulations()
        {
            List<Simulation> simulations = new List<Simulation>();           

            var exps = from exp in context.Experiments select exp;
            foreach(var exp in exps)
            {
                var simulation = NewSorghumSimulation(exp.Name, exp.BeginDate, exp.EndDate);

                simulation.Add(GetField(exp.FieldId));
                
                simulations.Add(simulation);
            }

            return simulations;
        }

        public static Simulation NewSorghumSimulation(string name, DateTime? start, DateTime? end)
        {
            var simulation = new Simulation() { Name = name };

            simulation.Add(new Clock(){
                Name = "Clock",
                StartDate = start,
                EndDate = end
            });

            simulation.Add(new Summary() { Name = "SummaryFile" });            

            simulation.Add(new Weather(){ 
                Name = "HE1996",
                FileName = "HE1996.met"
            });

            simulation.Add(new SurfaceOrganicMatter() { Name = "SurfaceOrganicMatter" });

            return simulation;
        }
        private Zone GetField(int? fieldId)
        {
            var field = (from f in context.Fields where f.FieldId == fieldId select f).First();

            var zone = new Zone()
            {
                Name = field.Name,
                Slope = (double)field.Slope
            };            

            zone.Add(GetManagers());
            zone.Add(new Irrigation() { Name = "Irrigation" });
            zone.Add(new Fertiliser() { Name = "Fertiliser" });
            zone.Add(GetOperations());

            zone.Add(GetSoil(field.SoilId, field.Latitude, field.Longitude));

            zone.Add(new Plant() { Name = "Sorghum" });
            zone.Add(new Report() { Name = "Output file" });

            return zone;
        }

        private Folder GetManagers()
        {
            var folder = new Folder() { Name = "Manager folder" };

            folder.Add(new Memo()
            {
                Name = "Manage placeholder",
                Text = "Need to ask where this data is coming from"
            });

            return folder;
        }

        private Operations GetOperations()
        {
            int exp = 1;

            var model = new Operations();

            var iquery = context.Query.IrrigationsByExperiment(exp);
            var irrigations = iquery
                .Select(i => new Operation()
                    { 
                        Action = $"[Irrigation].Apply({i.Amount})",
                        Date = (DateTime)i.Date
                    }
                );

            var fquery = context.Query.FertilizationsByExperiment(exp);
            var fertilizations = fquery
                .Select(f => new Operation() 
                    {
                        Action = $"[Fertiliser].Apply({f.Amount}, {f.Fertilizer.Name}, {f.Depth})",
                        Date = (DateTime)f.Date
                    }
                );

            model.Operation?.AddRange(irrigations);
            model.Operation?.AddRange(fertilizations);

            return model;
        }

        private Soil GetSoil(int soilId, double? lat, double? lon)
        {
            var model = new Soil()
            {
                Name = "Soil",
                Latitude = (double)lat,
                Longitude = (double)lon
            };       

            model.Add(GetWater(soilId));
            model.Add(GetSoilWater(soilId));
            model.Add(GetSoilNitrogen());
            model.Add(GetSoilOrganicMatter(soilId));
            model.Add(GetChemicalAnalysis(soilId));
            model.Add(GetSample(soilId, "Initial Water"));
            model.Add(GetSample(soilId, "Initial Nitrogen"));
            model.Add(new CERESSoilTemperature() { Name = "ExampleSoilTemperature" });

            return model;
        }

        private Water GetWater(int soilId)
        {          
            var model = new Water()
            {
                Name = "Physical",
                Thickness = context.Query.SoilLayerThicknessBySoil(soilId),
                BD = context.Query.SoilLayerDataByTrait("BD", soilId),
                AirDry = context.Query.SoilLayerDataByTrait("AirDry", soilId),
                LL15 = context.Query.SoilLayerDataByTrait("LL15", soilId),
                DUL = context.Query.SoilLayerDataByTrait("DUL", soilId),
                SAT = context.Query.SoilLayerDataByTrait("SAT", soilId),
                KS = context.Query.SoilLayerDataByTrait("KS", soilId)
            };

            model.Add(GetSoilCrop(soilId));

            return model;
        }

        private SoilCrop GetSoilCrop(int soilId)
        {           
            return new SoilCrop() 
            { 
                Name = "SoilCrop",
                LL = context.Query.SoilLayerDataByTrait("LL", soilId),
                KL = context.Query.SoilLayerDataByTrait("KL", soilId),
                XF = context.Query.SoilLayerDataByTrait("XF", soilId)
            };
        }

        private SoilWater GetSoilWater(int soilId)
        {
            return new SoilWater() 
            { 
                Name = "SoilWater",
                Thickness = context.Query.SoilLayerThicknessBySoil(soilId),
                SWCON = context.Query.SoilLayerDataByTrait("SWCON", soilId),
                KLAT = context.Query.SoilLayerDataByTrait("KLAT", soilId)
            };
            // TODO: Initiliase single parameters           
        }        

        private SoilNitrogen GetSoilNitrogen()
        {
            var model = new SoilNitrogen() { Name = "SoilNitrogen" };

            model.Add(new SoilNitrogenNH4() { Name = "NH4" });
            model.Add(new SoilNitrogenNO3() { Name = "NO3" });
            model.Add(new SoilNitrogenUrea() { Name = "Urea" });
            model.Add(new SoilNitrogenPlantAvailableNH4() { Name = "PlantAvailableNH4" });
            model.Add(new SoilNitrogenPlantAvailableNO3() { Name = "PlantAvailableNO3" });

            return model;
        }

        private Organic GetSoilOrganicMatter(int soilId)
        {
            return new Organic() 
            { 
                Name = "Organic",
                Thickness = context.Query.SoilLayerThicknessBySoil(soilId),
                Carbon = context.Query.SoilLayerDataByTrait("Carbon", soilId),
                SoilCNRatio = context.Query.SoilLayerDataByTrait("SoilCNRatio", soilId),
                FBiom = context.Query.SoilLayerDataByTrait("FBiom", soilId),
                FInert = context.Query.SoilLayerDataByTrait("FInert", soilId),
                FOM = context.Query.SoilLayerDataByTrait("FOM", soilId)
            };
        }

        private Analysis GetChemicalAnalysis(int soilId)
        {
            return new Analysis()
            {
                Name = "Chemical",
                PH = context.Query.SoilLayerDataByTrait("PH", soilId)
            };
        }

        private Sample GetSample(int soilId, string name)
        {
            return new Sample()
            {
                Name = name
            };
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
