using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.Met.Queries
{
    public class MetFileDataQueryHandler : IRequestHandler<MetFileDataQuery, IEnumerable<MetFileDataVm>>
    {
        private readonly IRemsDbContext _context;
        private readonly IMapper _mapper;

        public MetFileDataQueryHandler(IRemsDbFactory factory, IMapper mapper)
        {
            _context = factory.Context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MetFileDataVm>> Handle(MetFileDataQuery request, CancellationToken token)
        {
            var mets = _context.MetDatas.ToList()
                    .GroupBy(d => d.Date)
                    .Where(g => g.Count() == 4)
                    .ToList()
                    .Select(g =>
                    {
                        var data = new MetData[4];
                        data[0] = g.Single(d => d.Trait.Name == request.Map["MaxT"]);
                        data[1] = g.Single(d => d.Trait.Name == request.Map["MinT"]);
                        data[2] = g.Single(d => d.Trait.Name == request.Map["Radn"]);
                        data[3] = g.Single(d => d.Trait.Name == request.Map["Rain"]);

                        return data;
                    });            

            return mets
                .AsQueryable()
                .ProjectTo<MetFileDataVm>(_mapper.ConfigurationProvider);
        }
    }
}
