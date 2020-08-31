using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ExperimentsQuery : IRequest<KeyValuePair<int, string>[]>
    {
    }
}
