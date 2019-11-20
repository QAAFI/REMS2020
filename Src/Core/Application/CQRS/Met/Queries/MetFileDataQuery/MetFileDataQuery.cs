using MediatR;

using Rems.Application.Common.Mappings;

using System.Collections.Generic;

namespace Rems.Application.Met.Queries
{
    public class MetFileDataQuery : IRequest<IEnumerable<MetFileDataVm>>
    {
        public int Id { get; set; }

        public IPropertyMap Map { get; set; }
    }
}
