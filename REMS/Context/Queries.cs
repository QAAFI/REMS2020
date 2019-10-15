using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace REMS.Context
{
    using Entities;
    public class ContextQuerier 
    {
        private REMSContext context;
        public ContextQuerier(REMSContext context)
        {
            this.context = context;
        }
        public Trait FindTrait(string name)
        {
            var traits = from t in context.Traits
                        where t.Name == name
                        select t;

            return traits.First();
        }

        private IEnumerable<T> SoilLayerDataQuery<T>(Trait trait)
        {           



            var data = context.SoilLayerDatas;

            return null;
        }
    }
}
