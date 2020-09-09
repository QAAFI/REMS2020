using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Rems.Application.CQRS
{
    public class InsertOperationsTableCommand : IRequest<Unit>
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }
    }
}
