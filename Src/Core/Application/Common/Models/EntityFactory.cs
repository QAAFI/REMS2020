using System;
using System.Collections.Generic;
using System.Text;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Common.Models
{
    public static class EntityFactory
    {
        public static IEntity Create(string name)
        {
            switch (name)
            {
                case "ChemicalApplication":
                    return new ChemicalApplication() as IEntity;

                case "Crop":
                    return new Crop() as IEntity;

                case "Design":
                    return new Design() as IEntity;

                case "ExperimentInfo":
                    return new ExperimentInfo() as IEntity;

                case "Experiment":
                    return new Experiment() as IEntity;

                case "Factor":
                    return new Factor() as IEntity;

                case "FertilizationInfo":
                    return new FertilizationInfo() as IEntity;

                case "Fertilization":
                    return new Fertilization() as IEntity;

                case "Fertilizer":
                    return new Fertilizer() as IEntity;

                case "Field":
                    return new Field() as IEntity;

                case "Harvest":
                    return new Harvest() as IEntity;

                case "IrrigationInfo":
                    return new IrrigationInfo() as IEntity;

                case "Irrigation":
                    return new Irrigation() as IEntity;

                case "Level":
                    return new Level() as IEntity;

                case "MetData":
                    return new MetData() as IEntity;

                case "MetInfo":
                    return new MetInfo() as IEntity;

                case "MetStation":
                    return new MetStation() as IEntity;

                case "Method":
                    return new Method() as IEntity;

                case "PlotData":
                    return new PlotData() as IEntity;

                case "Plot":
                    return new Plot() as IEntity;

                case "Region":
                    return new Region() as IEntity;

                case "ResearcherList":
                    return new ResearcherList() as IEntity;

                case "Researcher":
                    return new Researcher() as IEntity;

                case "Site":
                    return new Site() as IEntity;

                case "SoilData":
                    return new SoilData() as IEntity;

                case "SoilLayerData":
                    return new SoilLayerData() as IEntity;

                case "SoilLayerTrait":
                    return new SoilLayerTrait() as IEntity;

                case "SoilLayer":
                    return new SoilLayer() as IEntity;

                case "SoilTrait":
                    return new SoilTrait() as IEntity;

                case "Soil":
                    return new Soil() as IEntity;

                case "Sowing":
                    return new Sowing() as IEntity;

                case "Stat":
                    return new Stat() as IEntity;

                case "TillageInfo":
                    return new TillageInfo() as IEntity;

                case "Tillage":
                    return new Tillage() as IEntity;

                case "Trait":
                    return new Trait() as IEntity;

                case "Treatment":
                    return new Treatment() as IEntity;

                case "Unit":
                    return new Unit() as IEntity;

                default:
                    throw new Exception("Entity type not recognised");
            }
        }
    }
}
