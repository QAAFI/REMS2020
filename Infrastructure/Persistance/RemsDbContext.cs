using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Persistence
{
    public class RemsDbContext: DbContext, IRemsDbContext
    {
        public string FileName { get; set; }

        public IEnumerable<string> Names
        {
            get
            {
                return Model.GetEntityTypes().Select(e => e.GetTableName());
            }
        }
        
        public RemsDbContext() { }

        public RemsDbContext(string filename) 
        {
            FileName = filename;       
        }

        public RemsDbContext(DbContextOptions<RemsDbContext> options)
            : base(options)
        { }        

        public DbSet<ChemicalApplication> ChemicalApplications { get; set; }

        public DbSet<Crop> Crops { get; set; }

        public DbSet<Design> Designs { get; set; }

        public DbSet<ExperimentInfo> ExperimentInfos { get; set; }

        public DbSet<Experiment> Experiments { get; set; }

        public DbSet<Factor> Factors { get; set; }

        public DbSet<FertilizationInfo> FertilizationInfos { get; set; }

        public DbSet<Fertilization> Fertilizations { get; set; }

        public DbSet<Fertilizer> Fertilizers { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<Harvest> Harvests { get; set; }

        public DbSet<IrrigationInfo> IrrigationInfos { get; set; }

        public DbSet<Irrigation> Irrigations { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<MetData> MetDatas { get; set; }

        public DbSet<MetInfo> MetInfos { get; set; }

        public DbSet<MetStation> MetStations { get; set; }

        public DbSet<Method> Methods { get; set; }

        public DbSet<PlotData> PlotData { get; set; }

        public DbSet<Plot> Plots { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<ResearcherList> ResearcherLists { get; set; }

        public DbSet<Researcher> Researchers { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<SoilData> SoilDatas { get; set; }

        public DbSet<SoilLayerData> SoilLayerDatas { get; set; }

        public DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        public DbSet<SoilLayer> SoilLayers { get; set; }

        public DbSet<SoilTrait> SoilTraits { get; set; }

        public DbSet<Soil> Soils { get; set; }

        public DbSet<Sowing> Sowings { get; set; }

        public DbSet<Stat> Stats { get; set; }

        public DbSet<TillageInfo> TillageInfos { get; set; }

        public DbSet<Tillage> Tillages { get; set; }

        public DbSet<Trait> Traits { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RemsDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseSqlite("Data Source=" + FileName);
                optionsBuilder.UseLazyLoadingProxies(true);
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.EnableDetailedErrors(true);
            }
        }

        public IEnumerable<T> GetSetAsEnumerable<T>(T t) where T : class, IEntity
        {
            var name = t.GetType().Name;
            switch (name)
            {
                case "ChemicalApplication":
                    return ChemicalApplications.Select(e => e as T);

                case "Crop":
                    return Crops.Select(e => e as T);

                case "Design":
                    return Designs.Select(e => e as T);

                case "ExperimentInfo":
                    return ExperimentInfos.Select(e => e as T);

                case "Experiment":
                    return Experiments.Select(e => e as T);

                case "Factor":
                    return Factors.Select(e => e as T);

                case "FertilizationInfo":
                    return FertilizationInfos.Select(e => e as T);

                case "Fertilization":
                    return Fertilizations.Select(e => e as T);

                case "Fertilizer":
                    return Fertilizers.Select(e => e as T);

                case "Field":
                    return Fields.Select(e => e as T);

                case "Harvest":
                    return Harvests.Select(e => e as T);

                case "IrrigationInfo":
                    return IrrigationInfos.Select(e => e as T);

                case "Irrigation":
                    return Irrigations.Select(e => e as T);

                case "Level":
                    return Levels.Select(e => e as T);

                case "MetData":
                    return MetDatas.Select(e => e as T);

                case "MetInfo":
                    return MetInfos.Select(e => e as T);

                case "MetStation":
                    return MetStations.Select(e => e as T);

                case "Method":
                    return Methods.Select(e => e as T);

                case "PlotData":
                    return PlotData.Select(e => e as T);

                case "Plot":
                    return Plots.Select(e => e as T);

                case "Region":
                    return Regions.Select(e => e as T);

                case "ResearcherList":
                    return ResearcherLists.Select(e => e as T);

                case "Researcher":
                    return Researchers.Select(e => e as T);

                case "Site":
                    return Sites.Select(e => e as T);

                case "SoilData":
                    return SoilDatas.Select(e => e as T);

                case "SoilLayerData":
                    return SoilLayerDatas.Select(e => e as T);

                case "SoilLayerTrait":
                    return SoilLayerTraits.Select(e => e as T);

                case "SoilLayer":
                    return SoilLayers.Select(e => e as T);

                case "SoilTrait":
                    return SoilTraits.Select(e => e as T);

                case "Soil":
                    return Soils.Select(e => e as T);

                case "Sowing":
                    return Sowings.Select(e => e as T);

                case "Stat":
                    return Stats.Select(e => e as T);

                case "TillageInfo":
                    return TillageInfos.Select(e => e as T);

                case "Tillage":
                    return Tillages.Select(e => e as T);

                case "Trait":
                    return Traits.Select(e => e as T);

                case "Treatment":
                    return Treatments.Select(e => e as T);

                case "Unit":
                    return Units.Select(e => e as T);

                default:
                    throw new Exception($"Could not find set of type {name}");
            }
        }
    }
}
