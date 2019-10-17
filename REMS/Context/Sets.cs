using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace REMS.Context
{
    using Entities;

    public partial class REMSContext : DbContext
    {
        [Set("ChemicalApplication", typeof(ChemicalApplication))]
        public virtual DbSet<ChemicalApplication> ChemicalApplications { get; set; }

        [Set("Crops", typeof(Crop))]
        public virtual DbSet<Crop> Crops { get; set; }

        [Set("Designs", typeof(Design))]
        public virtual DbSet<Design> Designs { get; set; }

        [Set("ExperimentInfo", typeof(ExperimentInfo))]
        public virtual DbSet<ExperimentInfo> ExperimentInfos { get; set; }

        [Set("Experiments", typeof(Experiment))]
        public virtual DbSet<Experiment> Experiments { get; set; }

        [Set("Factors", typeof(Factor))]
        public virtual DbSet<Factor> Factors { get; set; }

        [Set("FertilizationInfo", typeof(FertilizationInfo))]
        public virtual DbSet<FertilizationInfo> FertilizationInfos { get; set; }

        [Set("Fertilizations", typeof(Fertilization))]
        public virtual DbSet<Fertilization> Fertilizations { get; set; }

        [Set("Fertilizers", typeof(Fertilizer))]
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }

        [Set("Fields", typeof(Field))]
        public virtual DbSet<Field> Fields { get; set; }

        [Set("Harvests", typeof(Harvest))]
        public virtual DbSet<Harvest> Harvests { get; set; }

        [Set("IrrigationInfo", typeof(IrrigationInfo))]
        public virtual DbSet<IrrigationInfo> IrrigationInfos { get; set; }

        [Set("Irrigations", typeof(Irrigation))]
        public virtual DbSet<Irrigation> Irrigations { get; set; }

        [Set("Levels", typeof(Level))]
        public virtual DbSet<Level> Levels { get; set; }

        [Set("MetData", typeof(MetData))]
        public virtual DbSet<MetData> MetDatas { get; set; }

        [Set("MetInfo", typeof(MetInfo))]
        public virtual DbSet<MetInfo> MetInfos { get; set; }

        [Set("MetStations", typeof(MetStation))]
        public virtual DbSet<MetStation> MetStations { get; set; }

        [Set("Methods", typeof(Method))]
        public virtual DbSet<Method> Methods { get; set; }

        [Set("PlotData", typeof(PlotData))]
        public virtual DbSet<PlotData> PlotDatas { get; set; }

        [Set("Plots", typeof(Plot))]
        public virtual DbSet<Plot> Plots { get; set; }

        [Set("Regions", typeof(Region))]
        public virtual DbSet<Region> Regions { get; set; }

        [Set("ResearcherLists", typeof(ResearcherList))]
        public virtual DbSet<ResearcherList> ResearcherLists { get; set; }

        [Set("Researchers", typeof(Researcher))]
        public virtual DbSet<Researcher> Researchers { get; set; }

        [Set("Sites", typeof(Site))]
        public virtual DbSet<Site> Sites { get; set; }

        [Set("SoilData", typeof(SoilData))]
        public virtual DbSet<SoilData> SoilDatas { get; set; }

        [Set("SoilLayerData", typeof(SoilLayerData))]
        public virtual DbSet<SoilLayerData> SoilLayerDatas { get; set; }

        [Set("SoilLayerTraits", typeof(SoilLayerTrait))]
        public virtual DbSet<SoilLayerTrait> SoilLayerTraits { get; set; }

        [Set("SoilLayers", typeof(SoilLayer))]
        public virtual DbSet<SoilLayer> SoilLayers { get; set; }

        [Set("SoilTraits", typeof(SoilTrait))]
        public virtual DbSet<SoilTrait> SoilTraits { get; set; }

        [Set("Soils", typeof(Soil))]
        public virtual DbSet<Soil> Soils { get; set; }

        [Set("Stats", typeof(Stat))]
        public virtual DbSet<Stat> Stats { get; set; }

        [Set("TillageInfo", typeof(TillageInfo))]
        public virtual DbSet<TillageInfo> TillageInfos { get; set; }

        [Set("Tillage", typeof(Tillage))]
        public virtual DbSet<Tillage> Tillages { get; set; }

        [Set("Traits", typeof(Trait))]
        public virtual DbSet<Trait> Traits { get; set; }

        [Set("Treatments", typeof(Treatment))]
        public virtual DbSet<Treatment> Treatments { get; set; }

        [Set("Units", typeof(Unit))]
        public virtual DbSet<Unit> Units { get; set; }
    }
}
