using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Experiments.Queries.GetExperimentList
{
    class GetExperimentsListQueryHandler : IRequestHandler<GetExperimentsListQuery, ExperimentsListVm>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public GetExperimentsListQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperimentsListVm> Handle(GetExperimentsListQuery request, CancellationToken cancellationToken)
        {
            var experiments = await _context.Experiments
                .ProjectTo<ExperimentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ExperimentsListVm
            {
                Experiments = experiments
            };

            return vm;
        }
    }
}
