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

namespace Rems.Application.Treatments.Queries
{
    public class IrrigationsQueryHandler : IRequestHandler<IrrigationsQuery, IEnumerable<IrrigationDto>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public IrrigationsQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
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
