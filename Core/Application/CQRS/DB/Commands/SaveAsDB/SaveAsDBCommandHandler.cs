using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class SaveAsDBCommandHandler : IRequestHandler<SaveAsDBCommand>
    {
        IRemsDbContext _context;

        public SaveAsDBCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(SaveAsDBCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _context.SaveAs(request.FileName);
                return Unit.Value;
            });            
        }
    }
}
