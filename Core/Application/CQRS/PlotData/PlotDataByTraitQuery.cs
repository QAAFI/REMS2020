﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds data for a trait in a plot
    /// </summary>
    public class PlotDataByTraitQuery : TraitDataQuery<DateTime, double>
    {
        /// <summary>
        /// The source plot
        /// </summary>
        public int PlotId { get; set; }
                
        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotDataByTraitQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override SeriesData<DateTime, double> Run()
        {
            if (_context.Traits.FirstOrDefault(t => t.Name == TraitName) is not Trait trait)
                return null;

            if (_context.Plots.Find(PlotId) is not Plot plot)
                return null;

            IEnumerable<(DateTime Date, double Value)> data = trait.Type == "Soil"
                ? plot.SoilData.Where(d => d.Trait == trait).OrderBy(d => d.Date).Select(d => (d.Date, d.Value))
                : plot.PlotData.Where(d => d.Trait == trait).OrderBy(d => d.Date).Select(d => (d.Date, d.Value));

            string name = TraitName + " " + plot.Repetition;
            string units = trait?.Unit?.Name;

            var series = new SeriesData<DateTime, double>
            {
                Name = name,
                X = data.Select(d => d.Date).ToArray(),
                Y = data.Select(d => d.Value).ToArray(),
                XName = "Date",
                YName = $"Value ({units})"
            };

            return series;
        }
    }
}
