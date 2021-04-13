using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        /// <summary>
        /// Occurs when progress has been made on the insertion
        /// </summary>
        public Action IncrementProgress { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertPlotsCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var rows = Table.Rows.Cast<DataRow>();

            // Group the experiment rows together
            var eGroup = rows.GroupBy(row => row[0].ToString());

            var plots = new List<Plot>();

            foreach (var e in eGroup)
            {
                // Sub-group the experiment rows by treatment
                var tGroup = e.GroupBy(row => row[1].ToString());

                foreach (var t in tGroup)
                {
                    var levels = t.First().ItemArray
                        .Skip(4)
                        .Select(o => o.ToString().Replace(" ", "").ToUpper())
                        .ToArray();

                    var treatment = CreateTreatment(e.Key, t.Key, levels);

                    foreach (var row in t)
                    {
                        var plot = new Plot()
                        {
                            Treatment = treatment,
                            Repetition = Convert.ToInt32(row[2]),
                            Column = Convert.ToInt32(row[3])
                        };
                        plots.Add(plot);

                        IncrementProgress();
                    }
                }
            }

            _context.AttachRange(plots.ToArray());
            _context.SaveChanges();

            return Unit.Value;            
        }

        /// <summary>
        /// Creates a treatment entity and adds it to the database, along with its related levels
        /// and design
        /// </summary>
        private Treatment CreateTreatment(string exp, string name, string[] levels)
        {
            var treatment = new Treatment()
            {
                Experiment = _context.Experiments.Single(e => e.Name == exp),
                Name = name
            };
            _context.Add(treatment);
            _context.SaveChanges();

            foreach (var level in levels)
            {
                var entity = _context.Levels.FirstOrDefault(l => l.Name.Replace(" ", "").ToUpper() == level);

                if (entity is null) continue;

                var design = new Design()
                {
                    Level = entity,
                    TreatmentId = treatment.TreatmentId
                };
                _context.Add(design);
                _context.SaveChanges();
            }

            return treatment;            
        }

    }
}
