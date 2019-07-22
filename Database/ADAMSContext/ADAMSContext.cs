using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        public string Connection { get; }

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
    }
}
