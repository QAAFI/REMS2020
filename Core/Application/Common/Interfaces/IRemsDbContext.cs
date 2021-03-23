using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbContext
    {
        #region DbContext base

        IModel Model { get; }

        string FileName { get; set; }

        int SaveChanges();

        EntityEntry Add(object entity);

        EntityEntry Attach(object entity);

        void AddRange(params object[] entities);

        void AttachRange(params object[] entities);

        EntityEntry Entry(object entity);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        #endregion

        #region Sets

        DbSet<T> GetSet<T>() where T : class, IEntity;

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

        #endregion        

        IEnumerable<string> Names { get; }        
    }
}
