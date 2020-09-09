using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Rems.Application.CQRS
{
    public class InsertMetDataTableCommand : IRequest<Unit>
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }
    }
}
