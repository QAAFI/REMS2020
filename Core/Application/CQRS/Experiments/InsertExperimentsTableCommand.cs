﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
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
                var crop = _context.Crops.FirstOrDefault(c => c.Name == row.GetValue<string>("Crop"));

                var site = _context.Sites.FirstOrDefault(s => s.Name == row.GetValue<string>("SiteName"));
                var field = site.Fields.FirstOrDefault(f => f.Name == row.GetValue<string>("Field"));

                var met = _context.MetStations.FirstOrDefault(s => s.Name == row.GetValue<string>("MetStation"));
                
                var result = new Experiment
                {
                    Name = row.GetValue<string>("Name"),
                    Description = row.GetValue<string>("Description"),
                    Crop = crop,
                    Field = field,
                    BeginDate = row.GetValue<DateTime>("BeginDate"),
                    EndDate = row.GetValue<DateTime>("EndDate"),
                    MetStation = met,
                    Design = row.GetValue<string>("Design"),
                    Repetitions = row.GetValue<int>("Repetitions"),
                    Rating = row.GetValue<int>("Rating"),
                    Notes = row.GetValue<string>("Notes")
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