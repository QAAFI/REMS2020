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
        private readonly IRemsDbFactory _factory;
        private readonly IMapper _mapper;

        public GetExperimentsListQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<ExperimentsListVm> Handle(GetExperimentsListQuery request, CancellationToken cancellationToken)
        {
            var experiments = await _factory.Context.Experiments
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
