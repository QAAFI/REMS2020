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

            ChemicalApplication = Set<ChemicalApplication>();
            Crops = Set<Crop>();
            Designs = Set<Design>();
            ExperimentInfo = Set<ExperimentInfo>();
            Experiments = Set<Experiment>();
            Factors = Set<Factor>();
            FertilizationInfo = Set<FertilizationInfo>();
            Fertilizations = Set<Fertilization>();
            Fertilizers = Set<Fertilizer>();
            Fields = Set<Field>();
            Harvests = Set<Harvest>();
            IrrigationInfo = Set<IrrigationInfo>();
            Irrigations = Set<Irrigation>();
            Levels = Set<Level>();
            MetData = Set<MetData>();
            MetInfo = Set<MetInfo>();
            MetStations = Set<MetStations>();
            Methods = Set<Method>();
            PlotData = Set<PlotData>();
            Plots = Set<Plot>();
            Regions = Set<Region>();
            ResearcherLists = Set<ResearcherList>();
            Researchers = Set<Researcher>();
            Sites = Set<Site>();
            SoilData = Set<SoilData>();
            SoilLayerData = Set<SoilLayerData>();
            SoilLayerTraits = Set<SoilLayerTrait>();
            SoilLayers = Set<SoilLayer>();
            SoilTraits = Set<SoilTrait>();
            Soils = Set<Soil>();
            Stats = Set<Stats>();
            TillageInfo = Set<TillageInfo>();
            Tillage = Set<Tillage>();
            Traits = Set<Trait>();
            Treatments = Set<Treatment>();
            Units = Set<Unit>();
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
            var props = typeof(ADAMSContext)
                .GetProperties();

            var tables = props
                .Where(p => Attribute.IsDefined(p, typeof(Table)));

            var table = tables
                .FirstOrDefault(p => (p.GetCustomAttribute(typeof(Table)) as Table).Name == name);
                       
            
            var set = table
                .GetValue(this, null);

            dynamic dyn = set as DbSet<object>;

            var local = dyn.Local;

            return null;
        }

        public void ImportDataSet(DataSet data)
        {
            var tables = typeof(ADAMSContext)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(Table)));            

            foreach (DataTable table in data.Tables)
            {
                var info = tables
                    .FirstOrDefault(p => 
                    (p.GetCustomAttribute(typeof(Table)) as Table).Name == table.TableName);

                var relation = (info.GetCustomAttribute(typeof(Table)) as Table).Relation;

                

                dynamic set = info.GetValue(this);

                foreach (DataRow row in table.Rows)
                {
                    row.ItemArray.Take(5);
                }
            }

        }
    }
}
