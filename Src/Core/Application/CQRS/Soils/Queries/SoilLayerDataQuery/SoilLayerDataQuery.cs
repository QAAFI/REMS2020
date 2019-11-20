using MediatR;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerDataQuery : IRequest<double[]>
    {
        public int PlotId { get; set; }

        public string TraitName { get; set; }
    }
}