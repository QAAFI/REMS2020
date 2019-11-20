using MediatR;

using Rems.Application.Common.Mappings;

namespace Rems.Application.Queries
{
    public class SowingQuery : IRequest<SowingQueryDto>
    {
        public int Id { get; set; }
    }
}
