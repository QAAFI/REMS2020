using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public GetExperimentDetailQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExperimentDetailVm> Handle(GetExperimentDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Experiments
                .Include(i => i.Treatments)
                .ThenInclude(t=>t.Designs)
                .ThenInclude(d=>d.Level)
                .ThenInclude(l=>l.Factor)
                .SingleOrDefaultAsync(x => x.ExperimentId == request.Id);

            //entity.Treatments.Include(i => i.Treatments);
            //entity.Entry(entity).Collection(i => i.Treatments).Load();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Experiment), request.Id);
            }

            var vm = _mapper.Map<ExperimentDetailVm>(entity);

            
            return vm;
        }
    }
}
