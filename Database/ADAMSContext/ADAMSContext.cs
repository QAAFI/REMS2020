using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        public string Connection { get; }

        public static List<string> TableNames
        {
            get
            {
                return typeof(ADAMSContext)                    
                    .GetProperties()                    
                    .Where(p => Attribute.IsDefined(p, typeof(Table)))
                    .Select(p => p.Name)
                    .ToList();
            }
        }

        public ADAMSContext(string connection)
        {
            Connection = connection;
        }

        public ADAMSContext(DbContextOptions<ADAMSContext> options)
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
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            BuildChemicalApplication(modelBuilder);
            BuildCrop(modelBuilder);
            BuildDesign(modelBuilder);
            BuildExperiment(modelBuilder);
            BuildExperimentInfo(modelBuilder);
            BuildFactor(modelBuilder);
            BuildFertilization(modelBuilder);
            BuildFertilizer(modelBuilder);
            BuildFertilizerInfo(modelBuilder);
            BuildField(modelBuilder);
            BuildHarvest(modelBuilder);
            BuildIrrigation(modelBuilder);
            BuildIrrigationInfo(modelBuilder);
            BuildLevel(modelBuilder);
            BuildMetData(modelBuilder);
            BuildMetInfo(modelBuilder);
            BuildMetStation(modelBuilder);
            BuildMethod(modelBuilder);
            BuildPlot(modelBuilder);
            BuildPlotData(modelBuilder);
            BuildRegion(modelBuilder);
            BuildResearcher(modelBuilder);
            BuildResearcherList(modelBuilder);
            BuildSite(modelBuilder);
            BuildSoil(modelBuilder);
            BuildSoilData(modelBuilder);
            BuildSoilLayer(modelBuilder);
            BuildSoilLayerData(modelBuilder);
            BuildSoilLayerTrait(modelBuilder);
            BuildSoilTrait(modelBuilder);
            BuildStats(modelBuilder);
            BuildTillage(modelBuilder);
            BuildTillageInfo(modelBuilder);
            BuildTrait(modelBuilder);
            BuildTreatment(modelBuilder);
            BuildUnit(modelBuilder);            
        }

        public void ImportDataSet(DataSet data)
        {
            var tables = typeof(ADAMSContext)
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
