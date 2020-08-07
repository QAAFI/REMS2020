using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System.Data;
using System.Collections.Generic;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotDataByTraitQuery : IRequest<DataTable>
    {
        public int PlotId { get; set; }

        public string TraitName { get; set; }
    }
}
