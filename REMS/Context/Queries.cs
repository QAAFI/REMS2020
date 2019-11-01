using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace REMS.Context
{
    using Entities;
    public partial class REMSContext 
    {      
        public class Queries
        {
            private REMSContext context;

            public Queries(REMSContext context)
            {
                this.context = context;
            }

            public List<double> SoilLayerThicknessBySoil(int soilId)
            {
                var widths = from layer in context.SoilLayers 
                             where layer.SoilId == soilId 
                             select (double)((layer.DepthTo ?? 0) - (layer.DepthFrom ?? 0));

                return widths.ToList();
            }

            public List<double> SoilLayerDataByTrait(string traitName, int soilId)
            {
                var trait = (from t in context.Traits
                             where t.Name == traitName
                             select t).FirstOrDefault();

                var values = (from slt in context.SoilLayerTraits
                       where slt.Trait == trait
                       where slt.SoilLayer.SoilId == soilId
                       orderby slt.SoilLayer.DepthFrom
                       select slt.Value ?? 0.0)
                       .ToList();

                // Apsim doesn't like lists of zeros, needs empty list
                if (values.Any(v => v != 0.0)) values.Clear();

                return values;
            }

            public IQueryable<Fertilization> FertilizationsByTreatment(Treatment treatment)
            {
                return from f in context.Fertilizations
                       where f.Treatment == treatment
                       select f;
            }

            public IQueryable<Irrigation> IrrigationsByTreatment(Treatment treatment)
            {
                return from i in context.Irrigations
                       where i.Treatment == treatment
                       select i;
            }
        }
    }
}
