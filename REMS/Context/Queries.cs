using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace REMS.Context
{
    using Entities;
    public partial class REMSContext 
    {
        

        public IQueryable<Fertilization> GetFertilizations(int id) => Query.FertilizationsByExperiment(id);

        public class Queries
        {
            private REMSContext context;

            public Queries(REMSContext context)
            {
                this.context = context;
            }

            public IEnumerable<double?> SoilLayerThicknessBySoil(int soilId)
            {
                var widths = from layer in context.SoilLayers 
                             where layer.SoilId == soilId 
                             select layer.DepthTo - layer.DepthFrom;

                return widths.Select(w => (double?)w);
            }

            public IEnumerable<double?> SoilLayerDataByTrait(string traitName, int soilId)
            {
                var trait = (from t in context.Traits
                             where t.Name == traitName
                             select t).FirstOrDefault();

                return from slt in context.SoilLayerTraits
                       where slt.Trait == trait
                       where slt.SoilLayer.SoilId == soilId
                       orderby slt.SoilLayer.DepthFrom
                       select slt.Value;                      
            }

            public IQueryable<Fertilization> FertilizationsByExperiment(int experimentId)
            {
                return from f in context.Fertilizations
                       where (
                         from t in context.Treatments
                         where t.TreatmentId == f.TreatmentId
                         select t.ExperimentId == experimentId
                       ).Any()
                       select f;
            }

            public IQueryable<Irrigation> IrrigationsByExperiment(int experimentId)
            {
                return from i in context.Irrigations
                       where (
                         from t in context.Treatments
                         where t.TreatmentId == i.TreatmentId
                         select t.ExperimentId == experimentId
                       ).Any()
                       select i;
            }
        }
    }
}
