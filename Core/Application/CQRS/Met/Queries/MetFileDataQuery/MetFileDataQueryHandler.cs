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
using Rems.Application.DB.Queries;

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
                    .OrderBy(d => d.Key)
                    .Where(g => g.Count() == 4) // TODO: Make this query less bad. It works but it hurts.
                    .ToList()
                    .Select(g =>
                    {
                        var data = new MetData[4];

                        data[0] = GetData(g, "MaxT");
                        data[1] = GetData(g, "MinT");
                        data[2] = GetData(g, "Radn");
                        data[3] = GetData(g, "Rain");

                        return data;
                    });            

            return mets
                .AsQueryable()
                .ProjectTo<MetFileDataVm>(_mapper.ConfigurationProvider);
        }

        private MetData GetData(IGrouping<System.DateTime, MetData> group, string name)
        {
            if (group.Any(d => d.Trait.Name == name)) 
                return group.Single(d => d.Trait.Name == name);
            else
            {
                var args = new EntityNotFoundArgs()
                {
                    Options = _context.Traits.Select(t => t.Name).ToArray(),
                    Name = name
                };

                var trait = _context.Traits.FirstOrDefault(t => t.Name == EventManager.InvokeEntityNotFound(null, args));
                trait.Name = name;
                _context.SaveChanges();

                return group.Single(d => d.Trait.Name == name);
            }
        }
    }
}
