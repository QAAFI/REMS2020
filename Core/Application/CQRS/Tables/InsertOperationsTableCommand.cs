using System;
using System.Collections;
using System.Collections.Generic;
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

            // Group rows by experiment
            var es = Table.Rows.OfType<DataRow>().GroupBy(r => r["Experiment"].ToString());

            foreach (var e in es)
            {
                var exp = _context.Experiments.First(x => x.Name == e.Key);

                var ops = new Dictionary<string, IList<object>>();
                var all = new List<DataRow>();

                void addOp(string key, DataRow row)
                {
                    if (!ops.ContainsKey(key))
                        ops.Add(key, new List<object>());

                    ops[key].Add(row.ToEntity(_context, Type, infos.ToArray()));
                }

                foreach (var row in e)
                {
                    var key = row["Treatment"].ToString();

                    if (key.ToUpper() == "ALL")
                        all.Add(row);
                    else
                        addOp(key, row);                  

                    Progress.Increment(1);
                }

                // If any treatment has an invalid name
                if (ops.Keys.FirstOrDefault(k => !exp.Treatments.Any(t => t.Name == k)) is string s)
                    throw new Exception($"No treatment {s} exists in experiment {exp.Name}," +
                        $" {Table.TableName}s could not be imported");

                var entities = exp.Treatments.Select(t =>
                {
                    foreach (var row in all)
                        addOp(t.Name, row);

                    var old = (op.GetValue(t) as IEnumerable).Cast<object>();
                    
                    var extras = old.Where(o => !ops[t.Name].Contains(o));
                    var news = ops[t.Name].Where(o => !old.Contains(o));
                    news.ForEach(o => id.SetValue(o, t.TreatmentId));

                    return (extras, news);
                });

                string msg = $"Changes detected to {Table.TableName}s in {e.Key}. " +
                            $"Continuing the import will override existing data.\n" +
                            $"Do you wish to proceed?";

                var list = entities.Where(e => e.extras.Any());
                if (list.Any() && Confirmer.Confirm(msg))
                    list.ForEach(l => _context.RemoveRange(l.extras));

                foreach (var (extras, news) in entities)
                    foreach (var o in news)
                        _context.Add(o);

                _context.SaveChanges();
            }

            return Unit.Value;            
        }
    }
}
