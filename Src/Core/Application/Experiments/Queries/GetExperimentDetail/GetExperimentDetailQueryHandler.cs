using AutoMapper;
using MediatR;
using Rems.Application.Common.Exceptions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Experiments.Queries.GetExperimentDetail
{
    public class GetExperimentDetailQueryHandler : IRequestHandler<GetExperimentDetailQuery, ExperimentDetailVm>
    {
        private readonly IRemsDbFactory _factory;
        private readonly IMapper _mapper;

        public GetExperimentDetailQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<ExperimentDetailVm> Handle(GetExperimentDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _factory.Context.Experiments
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Experiment), request.Id);
            }

            return _mapper.Map<ExperimentDetailVm>(entity);
        }
    }
}
