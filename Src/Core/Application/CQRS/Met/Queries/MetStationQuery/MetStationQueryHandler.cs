using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Met.Queries
{
    public class MetStationQueryHandler : IRequestHandler<MetStationQuery, MetStationDto>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public MetStationQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
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
