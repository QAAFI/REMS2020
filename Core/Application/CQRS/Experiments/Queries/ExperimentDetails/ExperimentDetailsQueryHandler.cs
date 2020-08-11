using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Queries
{
    public class ExperimentDetailsQueryHandler : IRequestHandler<ExperimentDetailsQuery, IEnumerable<ExperimentDetailVm>>
    {
        private readonly IRemsDbFactory _factory;
        private readonly IMapper _mapper;

        public ExperimentDetailsQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExperimentDetailVm>> Handle(ExperimentDetailsQuery request, CancellationToken token)
        {
            return _factory.Context.Experiments.ProjectTo<ExperimentDetailVm>(_mapper.ConfigurationProvider);
        }
    }
}
