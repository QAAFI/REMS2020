using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;



namespace Rems.Application.Met.Queries
{
    public class MetStationQueryHandler : IRequestHandler<MetStationQuery, string>
    {
        private readonly IRemsDbContext _context;

        public MetStationQueryHandler(IRemsDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public Task<string> Handle(MetStationQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => _context.Experiments.Find(request.ExperimentId).MetStation.Name);
        }
    }
}
