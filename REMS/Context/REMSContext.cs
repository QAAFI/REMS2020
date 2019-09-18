using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace REMS
{
    public partial class REMSContext : DbContext
    {
        public string Connection { get; }

        public static List<string> TableNames
        {
            get
            {
                return typeof(REMSContext)                    
                    .GetProperties()                    
                    .Where(p => Attribute.IsDefined(p, typeof(Table)))
                    .Select(p => p.Name)
                    .ToList();
            }
        }

        public REMSContext(string connection)
        {
            Connection = connection;
        }

        public REMSContext(DbContextOptions<REMSContext> options)
            : base(options)
        {

        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ChemicalApplication.BuildModel(modelBuilder);
            Crop.BuildModel(modelBuilder);
            Design.BuildModel(modelBuilder);
            Experiment.BuildModel(modelBuilder);
            ExperimentInfo.BuildModel(modelBuilder);
            Factor.BuildModel(modelBuilder);
            Fertilization.BuildModel(modelBuilder);
            FertilizationInfo.BuildModel(modelBuilder);
            Fertilizer.BuildModel(modelBuilder);            
            Field.BuildModel(modelBuilder);
            Harvest.BuildModel(modelBuilder);
            Irrigation.BuildModel(modelBuilder);
            IrrigationInfo.BuildModel(modelBuilder);
            Level.BuildModel(modelBuilder);
            MetData.BuildModel(modelBuilder);
            MetInfo.BuildModel(modelBuilder);
            MetStation.BuildModel(modelBuilder);
            Method.BuildModel(modelBuilder);
            Plot.BuildModel(modelBuilder);
            PlotData.BuildModel(modelBuilder);
            Region.BuildModel(modelBuilder);
            Researcher.BuildModel(modelBuilder);
            ResearcherList.BuildModel(modelBuilder);
            Site.BuildModel(modelBuilder);
            Soil.BuildModel(modelBuilder);
            SoilData.BuildModel(modelBuilder);
            SoilLayer.BuildModel(modelBuilder);
            SoilLayerData.BuildModel(modelBuilder);
            SoilLayerTrait.BuildModel(modelBuilder);
            SoilTrait.BuildModel(modelBuilder);
            Stat.BuildModel(modelBuilder);
            Tillage.BuildModel(modelBuilder);
            TillageInfo.BuildModel(modelBuilder);
            Trait.BuildModel(modelBuilder);
            Treatment.BuildModel(modelBuilder);
            Unit.BuildModel(modelBuilder);
        }

        public void ImportDataSet(DataSet data)
        {
            var tables = typeof(REMSContext)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(Table)));            

            foreach (DataTable table in data.Tables)
            {
                var setInfo = tables
                    .FirstOrDefault(p => 
                    (p.GetCustomAttribute(typeof(Table)) as Table).Name == table.TableName);

                //TODO: Alert the user if no table is found
                if (setInfo == null) continue;                

                AddTable(table, setInfo);
            }

            SaveChanges();
        }

        private void AddTable(DataTable table, PropertyInfo info)
        {
            // Find the type of relation
            var relation = (info.GetCustomAttribute(typeof(Table)) as Table).Relation;         

            // Find the DbSet to add the data to
            dynamic set = info.GetValue(this);

            // Iterate over the rows
            foreach (DataRow row in table.Rows)
            {
                // Clean the data
                var data = row.ItemArray.Select(i => ConvertDBNull(i)).ToArray();

                // Create a blank entity to store data in
                dynamic entity = Activator.CreateInstance(relation, data);
                set.Add(entity);
            }
            
        }

        /// <summary>
        /// Converts DBNull objects into null references
        /// </summary>
        private dynamic ConvertDBNull(dynamic item)
        {
            if (item.GetType() == typeof(DBNull)) return null;
            else return item;
        }
    }
}
