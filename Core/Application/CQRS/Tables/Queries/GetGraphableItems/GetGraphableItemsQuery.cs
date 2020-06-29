using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Tables.Queries
{
    public class GetGraphableItemsQuery : IRequest<string[]> 
    {
        public string TableName { get; set; }
    }
}
