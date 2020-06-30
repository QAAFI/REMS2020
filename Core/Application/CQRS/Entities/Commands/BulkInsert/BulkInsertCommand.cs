using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommand : IRequest<bool>
    {
        public DataSet Data { get; set; }

        public IPropertyMap TableMap { get; set; }        
    }
}
