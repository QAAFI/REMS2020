using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a table of MetData into the database
    /// </summary>
    public class InsertExperimentsTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source table
        /// </summary>
        public DataTable Table { get; set; }     

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertExperimentsTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            Experiment convertRow(DataRow row)
            {
                var crop = _context.Crops.FirstOrDefault(c => c.Name == Convert.ToString(row["Crop"]));

                var site = _context.Sites.FirstOrDefault(s => s.Name == Convert.ToString(row["SiteName"]));
                var field = site.Fields.FirstOrDefault(f => f.Name == Convert.ToString(row["Field"]));

                var met = _context.MetStations.FirstOrDefault(s => s.Name == Convert.ToString(row["MetStation"]));
                
                var result = new Experiment
                {
                    Name = Convert.ToString(row["Name"]),
                    Description = Convert.ToString(row["Description"]),
                    Crop = crop,
                    Field = field,
                    BeginDate = Convert.ToDateTime(row["BeginDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"]),
                    MetStation = met,
                    Design = Convert.ToString(row["Design"]),
                    Repetitions = Convert.ToInt32(row["Repetitions"]),
                    Rating = Convert.ToInt32(row["Rating"]),
                    Notes = Convert.ToString(row["Notes"])
            };

                Progress.Increment(1);

                return result;
            }

            var datas = Table.Rows.Cast<DataRow>()
                .Select(r => convertRow(r))
                .Distinct();

            if (_context.Experiments.Any())
                datas = datas.Except(_context.Experiments, new ExperimentComparer());

            _context.AddRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
        
    }

    /// <summary>
    /// Compares MetData entities based on the values of some properties
    /// </summary>
    internal class ExperimentComparer : IEqualityComparer<Experiment>
    {
        public bool Equals(Experiment x, Experiment y)
        {
            return x.Name == y.Name
                && x.BeginDate == y.BeginDate
                && x.EndDate == y.EndDate;
        }

        public int GetHashCode(Experiment obj)
        {
            var nHash = obj.Name.GetHashCode();
            var bHash = obj.BeginDate.GetHashCode();
            var eHash = obj.EndDate.GetHashCode();

            return Utilities.GenerateHash(nHash, bHash, eHash);
        }
    }
}
