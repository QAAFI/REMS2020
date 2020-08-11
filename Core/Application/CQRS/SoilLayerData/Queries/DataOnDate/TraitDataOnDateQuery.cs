using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System;
using System.Data;
using System.Collections.Generic;
using Rems.Application.Common;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class TraitDataOnDateQuery : IRequest<SeriesData>
    {
        public DateTime Date { get; set; }

        public int PlotId { get; set; }

        public string TraitName { get; set; }
    }
}
