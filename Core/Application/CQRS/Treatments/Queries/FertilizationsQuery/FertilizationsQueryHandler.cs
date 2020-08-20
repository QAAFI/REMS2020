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
    public class FertilizationsQueryHandler : IRequestHandler<FertilizationsQuery, IEnumerable<FertilizationDto>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public FertilizationsQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<FertilizationDto>> Handle(FertilizationsQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _context.Fertilizations
                .Where(f => f.TreatmentId == request.TreatmentId)
                .ProjectTo<FertilizationDto>(_mapper.ConfigurationProvider)
                .AsEnumerable()
            );                  
        }
    }    
}