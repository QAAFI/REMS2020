using System;
using System.Collections.Generic;
using System.Linq;
using Models.Soils;
using Models.WaterModel;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Chemical model for an experiment
    /// </summary>
    public class SoilModelTraitsQuery : ContextQuery<Dictionary<string, double[]>>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public string[] Depth { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilModelTraitsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<string, double[]> Run()
        {
            var layers = _context.GetSoilLayers(ExperimentId);

            double[] thickness = null;
            if (layers.Any())
            {
                Depth = layers.Select(l => $"{l.FromDepth ?? 0}-{l.ToDepth}").ToArray();
                thickness = layers.Select(l => (double)(l.ToDepth - l.FromDepth)).ToArray();
            }

            var traits = new Dictionary<string, double[]>
            {
                { "Thickness", thickness },
                
                // Physical
                { nameof(Physical.BD), GetData(layers, nameof(Physical.BD)) },
                { nameof(Physical.AirDry), GetData(layers, nameof(Physical.AirDry)) },
                { nameof(Physical.LL15), GetData(layers, nameof(Physical.LL15)) },
                { nameof(Physical.DUL), GetData(layers, nameof(Physical.DUL)) },
                { nameof(Physical.SAT), GetData(layers, nameof(Physical.SAT)) },
                { nameof(Physical.KS), GetData(layers, nameof(Physical.KS)) },

                // SoilCrop
                { nameof(SoilCrop.LL), GetData(layers, nameof(SoilCrop.LL)) },
                { nameof(SoilCrop.KL), GetData(layers, nameof(SoilCrop.KL)) },
                { nameof(SoilCrop.XF), GetData(layers, nameof(SoilCrop.XF)) },

                // WaterBalance
                { nameof(WaterBalance.SWCON), GetData(layers, nameof(WaterBalance.SWCON)) },
                { nameof(WaterBalance.KLAT), GetData(layers, nameof(WaterBalance.KLAT)) },

                // Organic
                { nameof(Organic.Carbon), GetData(layers, nameof(Organic.Carbon)) },
                { nameof(Organic.SoilCNRatio), GetData(layers, nameof(Organic.SoilCNRatio)) },
                { nameof(Organic.FBiom), GetData(layers, nameof(Organic.FBiom)) },
                { nameof(Organic.FInert), GetData(layers, nameof(Organic.FInert)) },
                { nameof(Organic.FOM), GetData(layers, nameof(Organic.FOM)) },

                // Chemical
                { nameof(Chemical.NO3N), GetData(layers, nameof(Chemical.NO3N)) },
                { nameof(Chemical.NH4N), GetData(layers, nameof(Chemical.NH4N)) },
                { nameof(Chemical.PH), GetData(layers, nameof(Chemical.PH)) }
            };

            return traits;
        }

        /// <summary>
        /// Gets the requested trait data for the given soil layers
        /// </summary>
        private double[] GetData(SoilLayer[] layers, string name)
        {
            var trait = _context.GetTraitByName(name);

            var traits = layers.Select(l => l.SoilLayerTraits.FirstOrDefault(t => t.TraitId == trait.TraitId))
                .Where(t => t != null)
                .Select(v => v.Value.GetValueOrDefault());

            var data = _context.SoilLayerDatas.Where(d => d.TraitId == trait.TraitId).ToArray();

            var datas = layers.Select(l => data.Where(d => d.DepthFrom * 10 == l.FromDepth)
                    .OrderBy(d => d.Date)
                    .FirstOrDefault()?.Value ?? 0
            );

            var values = traits ?? datas;

            return values.Any() ? values.ToArray() : null;
        }
    }
}
