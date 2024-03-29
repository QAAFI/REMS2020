﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using Models.Climate;
using System.IO;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Weather model for an experiment
    /// </summary>
    public class WeatherQuery : ContextQuery<Weather>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public (string Station, string File, DateTime Start, DateTime End)[] Mets { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<WeatherQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Weather Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            if (!exp.MetStation.MetData.Any())
            {
                Report.AddLine("No existing met data found. " +
                    "Either import met data and export again, " +
                    "or provide a valid file within APSIM NG before running the simulation.");

                return new();
            }

            var met = Mets.SingleOrDefault(m => m.Start <= exp.BeginDate && exp.EndDate <= m.End);

            if (met.Equals(default))
                Report.AddLine("The existing met data does not cover the duration of " +
                    "the experiment. Either import additional data or provide APSIM NG a " +
                    "valid .met file before running the simulation.");

            return new Weather { FileName = met.File };
        }
    }


    
}
