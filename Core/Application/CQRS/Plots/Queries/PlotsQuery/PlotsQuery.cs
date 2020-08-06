using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotsQuery : IRequest<IEnumerable<KeyValuePair<int, string>>>
    {
        public int TreatmentId { get; set; }
    }
}
