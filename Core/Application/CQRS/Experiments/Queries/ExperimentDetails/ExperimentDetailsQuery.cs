using MediatR;

using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.Queries
{
    public class ExperimentDetailsQuery : IRequest<IEnumerable<ExperimentDetailVm>>
    {
    }
}
