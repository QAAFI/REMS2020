using MediatR;

using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.Queries
{
    public class ExperimentsQuery : IRequest<IEnumerable<ExperimentDetailVm>>
    {
    }
}
