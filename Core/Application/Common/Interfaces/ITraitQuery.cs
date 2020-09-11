using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface ITraitQuery<T> : IRequest<T>
    {
        string TraitName { get; set; }
    }
}
