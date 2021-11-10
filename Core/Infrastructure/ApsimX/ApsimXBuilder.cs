using Models.Core;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimXBuilder : IRemsBuilder
    {
        public IQueryHandler Handler { get; set; }

        private readonly IModel simulations = new Simulations();

        public void Add(IRemsTemplate template)
        {
            throw new NotImplementedException();
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
