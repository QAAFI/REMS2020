using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace REMS.Context
{
    using Entities;

    public partial class REMSContext : DbContext
    {        
        public virtual DbSet<ChemicalApplication> ChemicalApplications { get; set; }
        
        public virtual DbSet<Crop> Crops { get; set; }
        
        public virtual DbSet<Design> Designs { get; set; }
        
        public virtual DbSet<ExperimentInfo> ExperimentInfos { get; set; }
        
        public virtual DbSet<Experiment> Experiments { get; set; }
        
        public virtual DbSet<Factor> Factors { get; set; }
        
        public virtual DbSet<FertilizationInfo> FertilizationInfos { get; set; }
        
        public virtual DbSet<Fertilization> Fertilizations { get; set; }
        
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }
        
        public virtual DbSet<Field> Fields { get; set; }
        
        public virtual DbSet<Harvest> Harvests { get; set; }
        
        public virtual DbSet<IrrigationInfo> IrrigationInfos { get; set; }
        
        public virtual DbSet<Irrigation> Irrigations { get; set; }
        
        public virtual DbSet<Level> Levels { get; set; }
        
        public virtual DbSet<MetData> MetDatas { get; set; }
        
        public virtual DbSet<MetInfo> MetInfos { get; set; }
        
        public virtual DbSet<MetStation> MetStations { get; set; }
        
        public virtual DbSet<Method> Methods { get; set; }
        
        public virtual DbSet<PlotData> PlotDatas { get; set; }
        
        public virtual DbSet<Plot> Plots { get; set; }
        
        public virtual DbSet<Region> Regions { get; set; }
        
        public virtual DbSet<ResearcherList> ResearcherLists { get; set; }
        
        public virtual DbSet<Researcher> Researchers { get; set; }
        
        public virtual DbSet<Site> Sites { get; set; }
        
        public virtual DbSet<SoilData> SoilDatas { get; set; }
        
        public virtual DbSet<SoilLayerData> SoilLayerDatas { get; set; }
        
        public virtual DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }
        
        public virtual DbSet<SoilLayer> SoilLayers { get; set; }
        
        public virtual DbSet<SoilTrait> SoilTraits { get; set; }
        
        public virtual DbSet<Soil> Soils { get; set; }
        
        public virtual DbSet<Stat> Stats { get; set; }
        
        public virtual DbSet<TillageInfo> TillageInfos { get; set; }
        
        public virtual DbSet<Tillage> Tillages { get; set; }
        
        public virtual DbSet<Trait> Traits { get; set; }
        
        public virtual DbSet<Treatment> Treatments { get; set; }
        
        public virtual DbSet<Unit> Units { get; set; }
    }
}
