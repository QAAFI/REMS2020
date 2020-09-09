using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System.Collections.Generic;
using System.Data;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class DesignsTableQuery : IRequest<DataTable>
    {
        public int ExperimentId { get; set; }
    }
}
