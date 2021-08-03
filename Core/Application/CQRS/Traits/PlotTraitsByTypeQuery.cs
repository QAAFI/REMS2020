using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the crop traits with data in a treatment
    /// </summary>
    public class PlotTraitsByTypeQuery : ContextQuery<string[]> 
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait type
        /// </summary>
        public string TraitType { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotTraitsByTypeQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override string[] Run()
        {
            IEnumerable<string> FindTraitNames(Plot plot)
            {
                switch (TraitType)
                {
                    case "Climate":
                        return _context.Traits.Where(t => t.Type == "Climate").Select(t => t.Name);

                    case "Soil":
                        return plot.SoilData.Select(d => d.Trait.Name);

                    case "SoilLayer":
                        return plot.SoilLayerData.Select(d => d.Trait.Name);

                    case "Crop":
                    default:
                        return plot.PlotData.Select(d => d.Trait.Name);
                }
            }

            var traits = _context.Treatments.Find(TreatmentId)
                .Plots
                .SelectMany(p => FindTraitNames(p))
                .Distinct()
                .ToArray();

            return traits;
        }
    }
}
