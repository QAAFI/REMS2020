using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Mappings;
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
        
        public IQueryable Query(string entity)
        {
            string name = "Rems.Domain.Entities." + entity;
            return Query(Model.FindEntityType(name).ClrType);
        }

        public IQueryable Query(Type entity)
        {
            return (IQueryable)((IDbSetCache)this).GetOrAddSet(this.GetDependencies().SetSource, entity);
        }

        
    }
}
