using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimXBuilder : IExportBuilder
    {
        public IRemsWriter Build(params IRemsData[] data)
        {
            var writer = new ApsimXWriter();

            foreach (var item in data)
                item.Write(writer);

            return writer;
        }
    }
}
