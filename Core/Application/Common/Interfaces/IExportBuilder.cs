using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IExportBuilder
    {
        public IRemsWriter Build(params IRemsData[] data);
    }
}
