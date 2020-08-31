using MediatR;

namespace Rems.Application.DB.Commands
{
    public class SaveAsDBCommand : IRequest
    { 
        public string FileName { get; set; }
    }
}
