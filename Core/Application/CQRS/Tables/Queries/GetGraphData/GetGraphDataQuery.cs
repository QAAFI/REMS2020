using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rems.Application.Tables.Queries
{
    public class GetGraphDataQuery : IRequest<IQueryable<Tuple<object, object>>> 
    {
        public string TableName { get; set; }

        public string TraitName { get; set; }

        public string XColumn { get; set; }

        public string YColumn { get; set; }
    }
}
