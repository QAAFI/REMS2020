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
    public class TreatmentDetailQueryHandler : IRequestHandler<TreatmentDetailQuery, TreatmentDetailVm>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public TreatmentDetailQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _context = factory.Context;
            _mapper = mapper;
        }

        public async Task<TreatmentDetailVm> Handle(TreatmentDetailQuery request, CancellationToken cancellationToken)
        {
            return _context.Treatments
                .Where(t => t.TreatmentId == request.Id)
                .ProjectTo<TreatmentDetailVm>(_mapper.ConfigurationProvider)
                .Single();
        }
    }
}
