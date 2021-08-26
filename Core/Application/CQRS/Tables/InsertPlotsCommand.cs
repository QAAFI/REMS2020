using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert plot information into the database
    /// </summary>
    public class InsertPlotsCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The table containing plot information
        /// </summary>
        public DataTable Table { get; set; }

        public IConfirmer Confirmer { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertPlotsCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            // Disable lazy loading for duplicate checking
            //_context.ChangeTracker.LazyLoadingEnabled = false;            

            var rows = Table.Rows.Cast<DataRow>();

            // Group the experiment rows together
            var eGroup = rows.GroupBy(row => row["Experiment"].ToString());

            foreach (var exp in eGroup)
            {
                // Sub-group the experiment rows by treatment
                var tGroup = exp.GroupBy(row => row["Treatment"].ToString());

                var experiment = _context.Experiments.Single(e => e.Name == exp.Key);

                var ts = tGroup.Select(t =>
                {
                    var treatment = new Treatment()
                    {
                        Experiment = _context.Experiments.Single(e => e.Name == experiment.Name),
                        Name = t.Key
                    };

                    // Find the treatment designs
                    treatment.Designs = t.First().ItemArray.Skip(4)
                        .Select(o => o.ToString().Replace(" ", "").ToUpper())
                        .Select(s => _context.Levels.FirstOrDefault(l => s == l.Name.Replace(" ", "").ToUpper()))
                        .Where(l => l is not null)
                        .Select(l => new Design { Level = l, Treatment = treatment })
                        .ToList();

                    // Find the treatment plots
                    treatment.Plots = t.Select(row => new Plot
                    {
                        Treatment = treatment,
                        Repetition = Convert.ToInt32(row["Repetition"]),
                        Column = Convert.ToInt32(row["Plot"])
                    }).ToList();

                    Progress.Increment(t.Count());
                    
                    return treatment;
                }).ToList();

                // Check for conflicts with existing treatments
                var comparer = new TreatmentComparer();
                
                var extras = experiment.Treatments.Where(t => !ts.Contains(t, comparer));

                string msg = $"Changes detected for treatments in {experiment.Name}. " +
                        $"Do you wish to replace the existing treatments? " +
                        $"Experiment data will need to be imported again.";

                if (extras.Any() && Confirmer.Confirm(msg))
                    _context.RemoveRange(extras);

                var news = ts.Where(t => !experiment.Treatments.Contains(t, comparer));
                foreach (var t in news)
                {
                    experiment.Treatments.Add(t);
                    _context.Add(t);
                }

                _context.SaveChanges();
            }

            return Unit.Value;            
        }

        private class TreatmentComparer : IEqualityComparer<Treatment>
        {
            public bool Equals(Treatment x, Treatment y)
                => x.Name == y.Name
                && x.Designs.SequenceEquivalent(y.Designs)
                && x.Plots.SequenceEquivalent(y.Plots);

            public int GetHashCode(Treatment obj) => obj.TreatmentId;
        }
    }
}
