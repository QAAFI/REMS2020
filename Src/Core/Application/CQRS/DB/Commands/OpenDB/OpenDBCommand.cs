using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.DB.Commands
{
    public class OpenDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }        
    }
}
