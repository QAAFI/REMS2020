using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Treatments.Queries
{
    public class IrrigationsQueryHandler : IRequestHandler<IrrigationsQuery, IEnumerable<IrrigationDto>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public IrrigationsQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _context = factory.Context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IrrigationDto>> Handle(IrrigationsQuery request, CancellationToken cancellationToken)
        {
            return _context.Fertilizations
                .Where(f => f.TreatmentId == request.TreatmentId)
                .ProjectTo<IrrigationDto>(_mapper.ConfigurationProvider);
        }
    }
}
