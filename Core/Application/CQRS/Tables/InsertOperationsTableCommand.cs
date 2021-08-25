using System;
using System.Collections;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a table of operations data to the database
    /// </summary>
    public class InsertOperationsTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public IConfirmer Confirmer { get; set; }

        public class Handler : BaseHandler<InsertOperationsTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            // Find the properties of each columnm assuming the first
            // two columns do not contain property data
            var infos = Table.Columns.Cast<DataColumn>()
                    .Skip(2)
                    .Select(c => c.FindProperty())
                    .Where(i => i != null)
                    .ToList();

            var id = Type.GetProperty("TreatmentId");
            var op = typeof(Treatment).GetProperty(Table.TableName + 's');

            object convertRow(DataRow row, Treatment treatment)
            {
                object entity = row.ToEntity(_context, Type, infos.ToArray()) as IComparable;
                id.SetValue(entity, treatment.TreatmentId);
                
                return entity;
            }

            // Group rows by experiment
            var es = Table.Rows.OfType<DataRow>().GroupBy(r => r["Experiment"].ToString());

            _context.ChangeTracker.LazyLoadingEnabled = false;

            foreach (var e in es)
            {
                var exp = _context.Experiments.First(x => x.Name == e.Key);

                // Group rows by treatment
                var ts = e.GroupBy(row => row["Treatment"].ToString()).SelectMany(group =>
                {
                    // Find the treatment/s the row references
                    var treats = group.Key.ToUpper() == "ALL"
                        ? exp.Treatments
                        : exp.Treatments.Where(treatment => treatment.Name == group.Key);

                    Progress.Increment(group.Count());

                    return treats.Select(treatment => 
                    (
                        ops: group.Select(row => convertRow(row, treatment)),
                        old: (op.GetValue(treatment) as IEnumerable).Cast<object>())
                    );
                }).ToList();

                string msg = $"Changes detected for operations in {e.Key}. " +
                            $"Continuing the import will override existing data.\n" +
                            $"Do you wish to proceed?";

                if (ts.Any(t => t.old.Any() && t.old.SequenceEquivalent(t.ops)))
                    if (!Confirmer.Confirm(msg))
                        throw new Exception("Import cancelled");
                    
                foreach (var (ops, old) in ts)
                {
                    _context.RemoveRange(old);

                    foreach (var obj in ops)
                        _context.Add(obj);
                }
            }

            _context.SaveChanges();

            return Unit.Value;            
        }
    }
}
