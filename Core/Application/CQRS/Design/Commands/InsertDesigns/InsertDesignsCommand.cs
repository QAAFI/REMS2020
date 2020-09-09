using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Rems.Application.CQRS
{
    public class InsertDesignsCommand : IRequest<Unit>
    {
        public DataTable Table { get; set; }
    }
}
