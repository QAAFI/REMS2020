using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IParameterised
    {
        void Parameterise(params object[] args);
    }
}
