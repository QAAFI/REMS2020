using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        public virtual DbSet<ChemicalApplication> ChemicalApplication { get; set; }
        public virtual DbSet<Crop> Crops { get; set; }

        public virtual DbSet<Design> Designs { get; set; }

        public virtual DbSet<ExperimentInfo> ExperimentInfo { get; set; }

        public virtual DbSet<Experiment> Experiments { get; set; }

        public virtual DbSet<Factor> Factors { get; set; }

        public virtual DbSet<FertilizationInfo> FertilizationInfo { get; set; }

        public virtual DbSet<Fertilization> Fertilizations { get; set; }

        public virtual DbSet<Fertilizer> Fertilizers { get; set; }

        public virtual DbSet<Field> Fields { get; set; }

        public virtual DbSet<Harvest> Harvests { get; set; }

        public virtual DbSet<IrrigationInfo> IrrigationInfo { get; set; }

        public virtual DbSet<Irrigation> Irrigations { get; set; }

        public virtual DbSet<Level> Levels { get; set; }

        public virtual DbSet<MetData> MetData { get; set; }

        public virtual DbSet<MetInfo> MetInfo { get; set; }

        public virtual DbSet<MetStations> MetStations { get; set; }

        public virtual DbSet<Method> Methods { get; set; }

        public virtual DbSet<PlotData> PlotData { get; set; }

        public virtual DbSet<Plot> Plots { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<ResearcherList> ResearcherLists { get; set; }

        public virtual DbSet<Researcher> Researchers { get; set; }

        public virtual DbSet<Site> Sites { get; set; }

        public virtual DbSet<SoilData> SoilData { get; set; }

        public virtual DbSet<SoilLayerData> SoilLayerData { get; set; }

        public virtual DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        public virtual DbSet<SoilLayer> SoilLayers { get; set; }

        public virtual DbSet<SoilTraits> SoilTraits { get; set; }

        public virtual DbSet<Soil> Soils { get; set; }

        public virtual DbSet<Stats> Stats { get; set; }

        public virtual DbSet<TillageInfo> TillageInfo { get; set; }

        public virtual DbSet<Tillage> Tillage { get; set; }

        public virtual DbSet<Trait> Traits { get; set; }

        public virtual DbSet<Treatment> Treatments { get; set; }

        public virtual DbSet<Unit> Units { get; set; }
    }
}
