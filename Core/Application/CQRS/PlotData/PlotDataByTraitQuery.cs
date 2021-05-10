﻿using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds data for a trait in a plot
    /// </summary>
    public class PlotDataByTraitQuery : ContextQuery<SeriesData>
    {
        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }

        /// <summary>
        /// The trait to search for
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotDataByTraitQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData Run()
        {
            var data = _context.PlotData
                .Where(p => p.Plot.PlotId == PlotId)
                .Where(p => p.Trait.Name == TraitName)
                .OrderBy(p => p.Date)
                .ToArray();

            if (data.Length == 0) return null;

            var rep = _context.Plots.Where(p => p.PlotId == PlotId);
            var x = rep.Select(p => p.Repetition).First();
            string name = TraitName + " " + x;

            SeriesData series = new SeriesData()
            {
                Name = name,
                X = new double[data.Count()],
                Y = new double[data.Count()],
                //X = Array.CreateInstance(typeof(DateTime), data.Count()),
                //Y = Array.CreateInstance(typeof(double), data.Count()),
                XName = "Value",
                YName = "Date"
            };

            for (int i = 0; i < data.Count(); i++)
            {
                series.X.SetValue(data[i].Date, i);
                series.Y.SetValue(data[i].Value, i);
            }

            return series;
        }
    }
}
