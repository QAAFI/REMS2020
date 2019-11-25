using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;



namespace Rems.Application.Met.Queries
{
    public class MetStationQueryHandler : IRequestHandler<MetStationQuery, MetStationDto>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public MetStationQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _context = factory.Context;
            _mapper = mapper;
        }

        public async Task<MetStationDto> Handle(MetStationQuery request, CancellationToken cancellationToken)
        {
            return _context.MetStations
                .Where(m => m.MetStationId == request.Id)
                .ProjectTo<MetStationDto>(_mapper.ConfigurationProvider)
                .Single();
        }
    }
}
