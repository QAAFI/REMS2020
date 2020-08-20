using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Interfaces;

namespace Rems.Persistence
{
    public class RemsDbFactory : IRemsDbFactory
    {
        public string Connection { get; set; }

        public IRemsDbContext Create()
        {
            return Create(Connection);
        }

        public IRemsDbContext Create(string filename)
        {
            Connection = filename;
            DbContext context = new RemsDbContext(filename);
            context.Database.EnsureCreated();

            return context as RemsDbContext;
        }       

    }
}
