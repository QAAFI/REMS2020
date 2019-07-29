using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        [Table("ChemicalApplication", typeof(ChemicalApplication))]
        public virtual DbSet<ChemicalApplication> ChemicalApplication { get; set; }

        [Table("Crops", typeof(Crop))]
        public virtual DbSet<Crop> Crops { get; set; }

        [Table("Designs", typeof(Design))]
        public virtual DbSet<Design> Designs { get; set; }

        [Table("ExperimentInfo", typeof(ExperimentInfo))]
        public virtual DbSet<ExperimentInfo> ExperimentInfo { get; set; }

        [Table("Experiments", typeof(Experiment))]
        public virtual DbSet<Experiment> Experiments { get; set; }

        [Table("Factors", typeof(Factor))]
        public virtual DbSet<Factor> Factors { get; set; }

        [Table("FertilizationInfo", typeof(FertilizationInfo))]
        public virtual DbSet<FertilizationInfo> FertilizationInfo { get; set; }

        [Table("Fertilizations", typeof(Fertilization))]
        public virtual DbSet<Fertilization> Fertilizations { get; set; }

        [Table("Fertilizers", typeof(Fertilizer))]
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }

        [Table("Fields", typeof(Field))]
        public virtual DbSet<Field> Fields { get; set; }

        [Table("Harvests", typeof(Harvest))]
        public virtual DbSet<Harvest> Harvests { get; set; }

        [Table("IrrigationInfo", typeof(IrrigationInfo))]
        public virtual DbSet<IrrigationInfo> IrrigationInfo { get; set; }

        [Table("Irrigations", typeof(Irrigation))]
        public virtual DbSet<Irrigation> Irrigations { get; set; }

        [Table("Levels", typeof(Level))]
        public virtual DbSet<Level> Levels { get; set; }

        [Table("MetData", typeof(MetData))]
        public virtual DbSet<MetData> MetData { get; set; }

        [Table("MetInfo", typeof(MetInfo))]
        public virtual DbSet<MetInfo> MetInfo { get; set; }

        [Table("MetStations", typeof(MetStations))]
        public virtual DbSet<MetStations> MetStations { get; set; }

        [Table("Methods", typeof(Method))]
        public virtual DbSet<Method> Methods { get; set; }

        [Table("PlotData", typeof(PlotData))]
        public virtual DbSet<PlotData> PlotData { get; set; }

        [Table("Plots", typeof(Plot))]
        public virtual DbSet<Plot> Plots { get; set; }

        [Table("Regions", typeof(Region))]
        public virtual DbSet<Region> Regions { get; set; }

        [Table("ResearcherLists", typeof(ResearcherList))]
        public virtual DbSet<ResearcherList> ResearcherLists { get; set; }

        [Table("Researchers", typeof(Researcher))]
        public virtual DbSet<Researcher> Researchers { get; set; }

        [Table("Sites", typeof(Site))]
        public virtual DbSet<Site> Sites { get; set; }

        [Table("SoilData", typeof(SoilData))]
        public virtual DbSet<SoilData> SoilData { get; set; }

        [Table("SoilLayerData", typeof(SoilLayerData))]
        public virtual DbSet<SoilLayerData> SoilLayerData { get; set; }

        [Table("SoilLayerTraits", typeof(SoilLayerTrait))]
        public virtual DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        [Table("SoilLayers", typeof(SoilLayer))]
        public virtual DbSet<SoilLayer> SoilLayers { get; set; }

        [Table("SoilTraits", typeof(SoilTrait))]
        public virtual DbSet<SoilTrait> SoilTraits { get; set; }

        [Table("Soils", typeof(Soil))]
        public virtual DbSet<Soil> Soils { get; set; }

        [Table("Stats", typeof(Stats))]
        public virtual DbSet<Stats> Stats { get; set; }

        [Table("TillageInfo", typeof(TillageInfo))]
        public virtual DbSet<TillageInfo> TillageInfo { get; set; }

        [Table("Tillage", typeof(Tillage))]
        public virtual DbSet<Tillage> Tillage { get; set; }

        [Table("Traits", typeof(Trait))]
        public virtual DbSet<Trait> Traits { get; set; }

        [Table("Treatments", typeof(Treatment))]
        public virtual DbSet<Treatment> Treatments { get; set; }

        [Table("Units", typeof(Unit))]
        public virtual DbSet<Unit> Units { get; set; }
    }
}
