using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.DB.Commands
{
    public class CreateDBCommand : IRequest<IRemsDbContext>
    {
        public string FileName { get; set; }        
    }
}
