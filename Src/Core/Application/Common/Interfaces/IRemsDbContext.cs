using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbContext
    {
        EntityEntry Add(object entity);

        void AddRange(params object[] entities);

        Task<int> SaveChangesAsync(CancellationToken token);

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<IPropertyMap> Mappings { get; set; }

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

        public DbSet<Sowing> Sowing { get; set; }

        public DbSet<Stat> Stats { get; set; }

        public DbSet<TillageInfo> TillageInfos { get; set; }

        public DbSet<Tillage> Tillages { get; set; }

        public DbSet<Trait> Traits { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Unit> Units { get; set; }
    }
}
