using Models.Factorial;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class FactorsQuery : ContextQuery<Factors>
    { 
        /// <summary>
        /// The experiment to model
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<FactorsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Factors Run()
        {
            var designs = _context.Designs.Where(d => d.Treatment.ExperimentId == ExperimentId);

            Factor convertEntity(Domain.Entities.Factor entity)
            {
                var factor = new Factor { Name = entity.Name };

                designs.Select(d => d.Level)
                    .Where(l => l.Factor == entity)
                    .Distinct()
                    .Select(l => new CompositeFactor { Name = l.Name, Specifications = GetSpecs(l) })
                    .ForEach(factor.Children.Add);

                string specification = "";

                switch (factor.Name)
                {
                    case "Cultivar":
                        specification = "[Sowing].Script.CultivarName = ";
                        break;

                    case "Sow Date":
                    case "Planting Date":
                        specification = "[Sowing].Script.SowDate = ";
                        break;

                    case "Row spacing":
                        specification = "[Sowing].Script.RowSpacing = ";
                        break;

                    case "Population":
                        specification = "";
                        break;

                    case "Nitrogen":
                    case "N Rates":
                    case "NRates":
                        specification = "[Fertilisation].Script.Amount = ";
                        break;

                    case "Treatment":
                    case "Density":
                    case "DayLength":
                    case "Irrigation":
                    default:
                        Report.AddLine("* No specification found for factor " + factor.Name);
                        break;
                }

                Action<CompositeFactor> specify = level =>
                {
                    if (!level.Specifications.Any())
                        level.Specifications.Add(specification + level.Name);
                };

                factor.Children.ForEach(specify);

                return factor;
            }
            
            var factors = designs.Select(d => d.Level.Factor).Distinct();
            var permutation = new Permutation();

            factors.Select(convertEntity).ForEach(permutation.Children.Add);
            
            var model = new Factors { Name = "Factors" };
            model.Children.Add(permutation);

            return model;
        }

        private static List<string> GetSpecs(Domain.Entities.Level level)
        {
            var specs = new List<string>();

            level.Specification?.Split(';')
                .Where(s => s != "")
                .ForEach(specs.Add);

            return specs;
        }
    }
}