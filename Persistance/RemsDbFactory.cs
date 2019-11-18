using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Persistence
{
    public class RemsDbFactory : IRemsDbFactory
    {
        public string FileName { get; set; }
        public IRemsDbContext Context { get; set; }

        public void Create(string filename)
        {
            var holder = new RemsDbContext(filename);

            holder.Database.EnsureCreated();
            holder.SaveChanges();

            Context = holder;
        }
        public void Open(string filename)
        {
            Context = new RemsDbContext(filename);

        }

    }
}
