using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Common.Extensions
{
    public static class RemsDbContextExtensions
    {
        public static void Close(this IRemsDbContext context)
        {
            /// TODO: Implement this
            //throw new NotImplementedException();
        }

        public static void Save(this IRemsDbContext context)
        {
            /// TODO: Implement this
            //throw new NotImplementedException();
        }

        public static void SaveAs(this IRemsDbContext context, string file)
        {
            /// TODO: Implement this
            //throw new NotImplementedException();
        }

        internal static Trait GetTraitByName(this IRemsDbContext context, string name)
        {
            var trait = context.Traits.FirstOrDefault(t => t.Name == name);

            if (trait == null)
            {
                var args = new ItemNotFoundArgs()
                {
                    Options = context.Traits.Select(t => t.Name).ToArray(),
                    Name = name
                };

                EventManager.InvokeItemNotFound(null, args);

                if (args.Selection == "None")
                {
                    trait = new Trait() { Name = name };
                    context.Add(trait);
                }
                else
                {
                    trait = context.Traits.FirstOrDefault(t => t.Name == args.Selection);
                    trait.Name = name;
                }
                
                context.SaveChanges();
            }

            return trait;
        }

        internal static SoilLayer[] GetSoilLayers(this IRemsDbContext context, int experimentId)
        {
            var soil = context.Experiments.Find(experimentId).Field.Soil;

            var layers = context.SoilLayers
                .Where(s => s.SoilId == soil.SoilId)
                .OrderBy(l => l.FromDepth)
                .ToArray();

            return layers;
        }

        internal static double[] GetSoilLayerTraitData(this IRemsDbContext context, SoilLayer[] layers, string name)
        {
            var trait = context.GetTraitByName(name);

            var data = layers.Select(l => l.SoilLayerTraits.FirstOrDefault(t => t.TraitId == trait.TraitId))
                .Where(v => v != null);
            return data.Select(v => v.Value.GetValueOrDefault()).ToArray();
        }
    }
}
