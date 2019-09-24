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
        
    }
}
