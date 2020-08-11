using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Tables.Queries
{
    public class TraitsByTypeQuery : IRequest<string[]> 
    {
        public string Type { get; set; }
    }
}
