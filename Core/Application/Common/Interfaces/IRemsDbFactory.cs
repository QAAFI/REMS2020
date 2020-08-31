using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbFactory
    {
        string Connection { get; set; }

        IRemsDbContext Create();

        IRemsDbContext Create(string filename);
    }
}
