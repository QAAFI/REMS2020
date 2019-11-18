using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbFactory
    {
        public IRemsDbContext Context { get; set; }

        public void Create(string filename);
        public void Open(string filename);



    }
}
