using Microsoft.EntityFrameworkCore;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rems.Infrastructure.Persistence
{
    /// <summary>
    /// Indicates that the marked property is of Type <see cref="DbSet{TEntity}"/>
    /// </summary>
    internal class EntitySet : Attribute
    { }

    public class RemsDbContext: DbContext, IRemsDbContext
    {
        public IEnumerable<string> Names => Model.GetEntityTypes().Select(e => e.GetTableName());

        private Dictionary<string, object> SetMap;

        public RemsDbContext(DbContextOptions<RemsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            DefineSets();
        }

        #region Sets

        [EntitySet]
        public DbSet<ChemicalApplication> ChemicalApplications { get; set; }

        [EntitySet]
        public DbSet<Crop> Crops { get; set; }

        [EntitySet]
        public DbSet<Design> Designs { get; set; }

        [EntitySet]
        public DbSet<ExperimentInfo> ExperimentInfos { get; set; }

        [EntitySet]
        public DbSet<Experiment> Experiments { get; set; }

        [EntitySet]
        public DbSet<Factor> Factors { get; set; }

        [EntitySet]
        public DbSet<FertilizationInfo> FertilizationInfos { get; set; }

        [EntitySet]
        public DbSet<Fertilization> Fertilizations { get; set; }

        [EntitySet]
        public DbSet<Fertilizer> Fertilizers { get; set; }

        [EntitySet]
        public DbSet<Field> Fields { get; set; }

        [EntitySet]
        public DbSet<Harvest> Harvests { get; set; }

        [EntitySet]
        public DbSet<IrrigationInfo> IrrigationInfos { get; set; }

        [EntitySet]
        public DbSet<Irrigation> Irrigations { get; set; }

        [EntitySet]
        public DbSet<Level> Levels { get; set; }

        [EntitySet]
        public DbSet<MetData> MetDatas { get; set; }

        [EntitySet]
        public DbSet<MetInfo> MetInfos { get; set; }

        [EntitySet]
        public DbSet<MetStation> MetStations { get; set; }

        [EntitySet]
        public DbSet<Method> Methods { get; set; }

        [EntitySet]
        public DbSet<PlotData> PlotData { get; set; }

        [EntitySet]
        public DbSet<Plot> Plots { get; set; }

        [EntitySet]
        public DbSet<Region> Regions { get; set; }

        [EntitySet]
        public DbSet<ResearcherList> ResearcherLists { get; set; }

        [EntitySet]
        public DbSet<Researcher> Researchers { get; set; }

        [EntitySet]
        public DbSet<Site> Sites { get; set; }

        [EntitySet]
        public DbSet<SoilData> SoilDatas { get; set; }

        [EntitySet]
        public DbSet<SoilLayerData> SoilLayerDatas { get; set; }

        [EntitySet]
        public DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        [EntitySet]
        public DbSet<SoilLayer> SoilLayers { get; set; }

        [EntitySet]
        public DbSet<SoilTrait> SoilTraits { get; set; }

        [EntitySet]
        public DbSet<Soil> Soils { get; set; }

        [EntitySet]
        public DbSet<Sowing> Sowings { get; set; }

        [EntitySet]
        public DbSet<Stat> Stats { get; set; }

        [EntitySet]
        public DbSet<TillageInfo> TillageInfos { get; set; }

        [EntitySet]
        public DbSet<Tillage> Tillages { get; set; }

        [EntitySet]
        public DbSet<Trait> Traits { get; set; }

        [EntitySet]
        public DbSet<Treatment> Treatments { get; set; }

        [EntitySet]
        public DbSet<Unit> Units { get; set; }

        #endregion

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RemsDbContext).Assembly);
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(true);
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.EnableDetailedErrors(true);
            }
        }        

        /// <summary>
        /// Returns the set of the corresponding type
        /// </summary>
        public DbSet<T> GetSet<T>() where T : class, IEntity => SetMap[typeof(T).Name] as DbSet<T>;

        /// <summary>
        /// Creates a map of entity type names to their corresponding <see cref="DbSet{TEntity}"/> property
        /// </summary>
        private void DefineSets()
        {
            var sets = GetType()
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(EntitySet)));

            SetMap = sets.ToDictionary(
                p => p.PropertyType.GetGenericArguments()[0].Name,
                p => p.GetValue(this));
        }
    }
}
