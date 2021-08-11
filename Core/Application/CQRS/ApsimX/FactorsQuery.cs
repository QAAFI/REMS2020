using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Fertiliser = Models.Fertiliser;
using Operation = Models.Operation;
using Operations = Models.Operations;
using Factor = Models.Factorial.Factor;
using Factors = Models.Factorial.Factors;
using CompositeFactor = Models.Factorial.CompositeFactor;
using Level = Rems.Domain.Entities.Level;
using Irrigation = Rems.Domain.Entities.Irrigation;
using Fertilization = Rems.Domain.Entities.Fertilization;

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
            var experiment = _context.Experiments.Find(ExperimentId);

            var treatments = new Factor { Name = "Treatment" };

            foreach (var treatment in experiment.Treatments)
            {
                var factor = new CompositeFactor { Name = treatment.Name};
                
                factor.Specifications = treatment.Designs.Select(d => d.Level)
                    .SelectMany(l => GetSpecification(l))
                    .ToList();

                var irrigs = treatment.Irrigations
                    .Select(i => new Operation { Date = i.Date.ToString(), Action = $"[Irrigation].Apply({i.Amount})" });

                AddOperations(factor, irrigs, "Irrigations");

                var ferts = treatment.Fertilizations
                    .Select(f => new Operation { Date = f.Date.ToString(), Action = FertAction(f) });

                AddOperations(factor, ferts, "Fertilisations");

                var unknowns = treatment.Fertilizations.Where(f => !Enum.IsDefined(typeof(Fertiliser.Types), f.Fertilizer.Name));
                foreach (var fert in unknowns.Select(f => f.Fertilizer.Name).Distinct())                
                {
                    Report.AddLine($"Matching APSIM fertiliser type for {fert}" +
                        $" not found in treatment {treatment.Name}. " +
                        $"Using default type instead (NO3N).\n");
                }

                treatments.Children.Add(factor);
            }
            
            var model = new Factors { Name = "Factors" };
            model.Children.Add(treatments);

            return model;
        }

        private string[] GetSpecification(Level level)
        {
            // Check for user-defined defined specifications
            if (level.Specification != null)
                return level.Specification.Split(';').Where(s => s != "").ToArray();

            // If there is no defined specification, construct one based on the level
            var name = level.Name.Replace('/', 'x');

            switch (level.Factor.Name.Replace(" ", "").ToLower())
            {
                case "nitrogen":
                case "nrates":
                    return new[] { "[Fertilisation].Script.Amount = " 
                        + Regex.Match(name, @"[0-9]*\.*[0-9]*").Value };

                case "irrigation":
                    return new[] { "[Irrigation].Script.Amount = "
                        + Regex.Match(name, @"[0-9]*\.*[0-9]*").Value };

                case "cultivar":
                    return new[] { "[Sowing].Script.Cultivar = " + name };

                case "date":
                case "sowdate":
                case "plantingdate":
                    return new[] { "[Sowing].Script.SowDate = " + name };

                case "rowpsace":
                case "rowspacing":
                    return new[] { "[Sowing].Script.RowSpacing = " + name };                

                case "pop":
                case "population":
                case "density":
                    return new[] { "[Sowing].Script.Density = " + name };

                case "depth":
                    return new[] { "[Sowing].Script.Depth = " + name };

                case "treatment":                
                case "daylength":
                default:
                    Report.AddLine("* No specification found for factor " + level.Factor.Name);
                    return new[] { "" };
            }
        }

        private void AddOperations(CompositeFactor factor, IEnumerable<Operation> items, string name)
        {
            if (!items.Any())
                return;

            factor.Specifications.Add($"[{name}]");
            var ops = new Operations{ Name = name };
            ops.Operation = items.ToList();
            factor.Children.Add(ops);
        }

        private string FertAction(Fertilization f)
        {
            var type = f.Fertilizer.Name;

            if (!Enum.IsDefined(typeof(Fertiliser.Types), type))
                type = type switch
                {
                    "Aqua ammonia" => Fertiliser.Types.NH4N.ToString(),
                    _ => Fertiliser.Types.NO3N.ToString(),
                };
            return $"[Fertiliser].Apply({f.Depth}, Fertiliser.Types.{type}, {f.Amount})";
        }
    }
}