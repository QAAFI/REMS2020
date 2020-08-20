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

        public TreatmentDetailQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<TreatmentDetailVm> Handle(TreatmentDetailQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _context.Treatments
                .Where(t => t.TreatmentId == request.Id)
                .ProjectTo<TreatmentDetailVm>(_mapper.ConfigurationProvider)
                .Single()
            );
        }
    }
}
