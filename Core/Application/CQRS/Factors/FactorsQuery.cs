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
            var model = new Factors { Name = "Factors" };

            var designs = _context.Designs.Where(d => d.Treatment.ExperimentId == ExperimentId);
            var factors = designs.Select(d => d.Level.Factor).Distinct();

            Factor convertEntity(Domain.Entities.Factor entity)
            {
                var factor = new Factor { Name = entity.Name };

                designs.Select(d => d.Level)
                    .Where(l => l.Factor == entity)
                    .Distinct()
                    .Select(l => new CompositeFactor { Name = l.Name, Specifications = GetSpecs(l) })
                    .ForEach(factor.Children.Add);

                return factor;
            }

            factors.Select(convertEntity).ForEach(model.Children.Add);

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