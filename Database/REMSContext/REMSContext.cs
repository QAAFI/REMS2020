using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Database
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
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            ChemicalApplication.Build(modelBuilder);
            Crop.Build(modelBuilder);
            Design.Build(modelBuilder);
            Experiment.Build(modelBuilder);
            ExperimentInfo.Build(modelBuilder);
            Factor.Build(modelBuilder);
            Fertilization.Build(modelBuilder);
            FertilizationInfo.Build(modelBuilder);
            Fertilizer.Build(modelBuilder);            
            Field.Build(modelBuilder);
            Harvest.Build(modelBuilder);
            Irrigation.Build(modelBuilder);
            IrrigationInfo.Build(modelBuilder);
            Level.Build(modelBuilder);
            MetData.Build(modelBuilder);
            MetInfo.Build(modelBuilder);
            MetStation.Build(modelBuilder);
            Method.Build(modelBuilder);
            Plot.Build(modelBuilder);
            PlotData.Build(modelBuilder);
            Region.Build(modelBuilder);
            Researcher.Build(modelBuilder);
            ResearcherList.Build(modelBuilder);
            Site.Build(modelBuilder);
            Soil.Build(modelBuilder);
            SoilData.Build(modelBuilder);
            SoilLayer.Build(modelBuilder);
            SoilLayerData.Build(modelBuilder);
            SoilLayerTrait.Build(modelBuilder);
            SoilTrait.Build(modelBuilder);
            Stat.Build(modelBuilder);
            Tillage.Build(modelBuilder);
            TillageInfo.Build(modelBuilder);
            Trait.Build(modelBuilder);
            Treatment.Build(modelBuilder);
            Unit.Build(modelBuilder);
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
