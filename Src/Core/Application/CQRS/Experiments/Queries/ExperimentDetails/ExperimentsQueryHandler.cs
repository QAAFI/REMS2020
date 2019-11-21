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

namespace Rems.Application.Queries
{
    public class ExperimentsQueryHandler : IRequestHandler<ExperimentsQuery, IEnumerable<ExperimentDetailVm>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public ExperimentsQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExperimentDetailVm>> Handle(ExperimentsQuery request, CancellationToken token)
        {
            return _context.Experiments.ProjectTo<ExperimentDetailVm>(_mapper.ConfigurationProvider);
        }
    }
}
