using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.DB.Commands
{
    public class OpenDBCommand : IRequest<Unit>
    {
        public string FileName { get; set; }        
    }
}
