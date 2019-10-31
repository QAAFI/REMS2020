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
            Simulations simulations = new Simulations();            

            var replacements = new Replacements() { Name = "Replacements" };
            replacements.Add(ApsimNode.ReadFromFile<Plant>("Sorghum.json"));

            simulations.Add(new DataStore());
            simulations.Add(replacements);
            simulations.Add(GetValidations());
            simulations.WriteToFile(file);
        }
        
        private Folder GetValidations()
        {
            Folder validation = new Folder() { Name = "Validations" };

            List<ApsimNode> experiments = new List<ApsimNode>();           

            var exps = from exp in context.Experiments select exp;            
            foreach(var exp in exps)
            {
                var experiment = new Folder() { Name = exp.Name};
                experiment.Add(GetTreatments(exp));
                experiments.Add(experiment);
            }
            validation.Add(experiments);

            return validation;
        }

        private IEnumerable<Simulation> GetTreatments(Context.Entities.Experiment experiment)
        {
            List<Simulation> simulations = new List<Simulation>();

            var treatments = from treatment in context.Treatments
                             where treatment.Experiment == experiment
                             select treatment;

            foreach (var treatment in treatments)
            {
                var simulation = NewSorghumSimulation(treatment);
                simulation.Add(GetField(treatment));
                simulations.Add(simulation);
            }

            return simulations;
        }

        public Simulation NewSorghumSimulation(Context.Entities.Treatment treatment)
        {
            var simulation = new Simulation() 
            { 
                Name = treatment.Name ?? "null"
            };

            simulation.Add(new Clock()
            {
                Name = "Clock",
                StartDate = treatment.Experiment.BeginDate,
                EndDate = treatment.Experiment.EndDate
            });

            simulation.Add(new Summary() 
            { 
                Name = "SummaryFile" 
            });            

            simulation.Add(new Weather()
            { 
                Name = treatment.Experiment.MetStation?.Name ?? "",
                FileName = treatment.Experiment.MetStation?.Name + ".met" ?? ""
            });            

            simulation.Add(new SurfaceOrganicMatter() { Name = "SurfaceOrganicMatter" });

            return simulation;
        }
        
        private Zone GetField(Context.Entities.Treatment treatment)
        {
            var field = treatment.Experiment.Field;

            var zone = new Zone()
            {
                Name = field.Name,
                Slope = (double)field.Slope
            };            

            zone.Add(GetManagers());
            zone.Add(new Irrigation() { Name = "Irrigation" });
            zone.Add(new Fertiliser() { Name = "Fertiliser" });
            zone.Add(GetOperations(treatment));
            zone.Add(GetSoil(field));
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

        private Operations GetOperations(Context.Entities.Treatment treatment)
        {
            var model = new Operations();

            var iquery = context.Query.IrrigationsByTreatment(treatment);
            var irrigations = iquery
                .Select(i => new Operation()
                    { 
                        Action = $"[Irrigation].Apply({i.Amount})",
                        Date = (DateTime)i.Date
                    }
                );

            var fquery = context.Query.FertilizationsByTreatment(treatment);
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

        private Soil GetSoil(Context.Entities.Field field)
        {
            var model = new Soil()
            {
                Name = "Soil",
                Latitude = (double)field.Latitude,
                Longitude = (double)field.Longitude
            };       

            model.Add(GetWater(field.SoilId));
            model.Add(GetSoilWater(field.SoilId));
            model.Add(GetSoilNitrogen());
            model.Add(GetSoilOrganicMatter(field.SoilId));
            model.Add(GetChemicalAnalysis(field.SoilId));
            model.Add(GetSample(field.SoilId, "Initial Water"));
            model.Add(GetSample(field.SoilId, "Initial Nitrogen"));
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
