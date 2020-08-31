using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Rems.Application.Queries
{
    public class ExperimentDetailsQueryHandler : IRequestHandler<ExperimentDetailsQuery, IEnumerable<ExperimentDetailVm>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public ExperimentDetailsQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<ExperimentDetailVm>> Handle(ExperimentDetailsQuery request, CancellationToken token)
        {
            return Task.Run(() =>
            {
                return _context.Experiments
                .ProjectTo<ExperimentDetailVm>(_mapper.ConfigurationProvider)
                .AsEnumerable();
            });
        }
    }
}
