using MediatR;

using Rems.Application.Common.Mappings;

using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Met.Queries
{
    public class MetFileDataQuery : IRequest<StringBuilder>
    {
        public int ExperimentId { get; set; }
    }
}
