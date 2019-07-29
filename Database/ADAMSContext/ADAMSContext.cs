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

        public DbSet<dynamic> GetTable(string name)
        {
            var table = typeof(ADAMSContext)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(Table)))
                .Where(p => (p.GetCustomAttribute(typeof(Table)) as Table).Name == name)
                .First()
                .GetValue(this) as DbSet<dynamic>;

            return table;
        }
    }
}
