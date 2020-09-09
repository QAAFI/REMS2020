using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.CQRS
{
    public class EntityTypesQuery : IRequest<Type>
    {
        public string Name { get; set; }
    }
}
