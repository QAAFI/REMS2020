using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common;

using System.Data;
using System.Collections.Generic;
using System.Numerics;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotDataTraitBoundsQuery : IRequest<PlotDataBounds>
    {
        public string TraitName { get; set; }
    }
}
