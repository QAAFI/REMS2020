using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ExperimentSummary : IRequest<Dictionary<string, string>>
    {
        public int ExperimentId { get; set; }
    }
}
