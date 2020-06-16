using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbFactory
    {
        IRemsDbContext Context { get; set; }

        void Create(string filename);

        void Open(string filename);

        DataTable getDataTable(string name);

    }
}
