using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Tables.Queries
{
    public class GetTraitNamesByIdQuery : IRequest<string[]> 
    {
        public string TraitIds { get; set; }
    }
}
