using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbContext
    {
        EntityEntry Add(object entity);

        void AddRange(params object[] entities);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token);

        IEnumerable<string> Names { get; }

        IModel Model { get; }

        DbSet<ChemicalApplication> ChemicalApplications { get; set; }

        DbSet<Crop> Crops { get; set; }

        DbSet<Design> Designs { get; set; }

        DbSet<ExperimentInfo> ExperimentInfos { get; set; }

        DbSet<Experiment> Experiments { get; set; }

        DbSet<Factor> Factors { get; set; }

        DbSet<FertilizationInfo> FertilizationInfos { get; set; }

        DbSet<Fertilization> Fertilizations { get; set; }

        DbSet<Fertilizer> Fertilizers { get; set; }

        DbSet<Field> Fields { get; set; }

        DbSet<Harvest> Harvests { get; set; }

        DbSet<IrrigationInfo> IrrigationInfos { get; set; }

        DbSet<Irrigation> Irrigations { get; set; }

        DbSet<Level> Levels { get; set; }

        DbSet<MetData> MetDatas { get; set; }

        DbSet<MetInfo> MetInfos { get; set; }

        DbSet<MetStation> MetStations { get; set; }
        
        DbSet<Method> Methods { get; set; }

        DbSet<PlotData> PlotData { get; set; }

        DbSet<Plot> Plots { get; set; }

        DbSet<Region> Regions { get; set; }

        DbSet<ResearcherList> ResearcherLists { get; set; }

        DbSet<Researcher> Researchers { get; set; }

        DbSet<Site> Sites { get; set; }

        DbSet<SoilData> SoilDatas { get; set; }

        DbSet<SoilLayerData> SoilLayerDatas { get; set; }

        DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        DbSet<SoilLayer> SoilLayers { get; set; }

        DbSet<SoilTrait> SoilTraits { get; set; }

        DbSet<Soil> Soils { get; set; }

        DbSet<Sowing> Sowings { get; set; }

        DbSet<Stat> Stats { get; set; }

        DbSet<TillageInfo> TillageInfos { get; set; }

        DbSet<Tillage> Tillages { get; set; }

        DbSet<Trait> Traits { get; set; }

        DbSet<Treatment> Treatments { get; set; }

        DbSet<Unit> Units { get; set; }

        IQueryable Query(string entity);

        IQueryable Query(Type entity);
    }
}
