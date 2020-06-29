using Microsoft.Data.Sqlite;
using Rems.Application.Common.Interfaces;
using System.Data;
using System.Linq;

namespace Rems.Persistence
{
    public class RemsDbFactory : IRemsDbFactory
    {
        public RemsDbFactory(IRemsDbContext context)
        {
            //webUI will add RemsDBContext to DI
            Context = context;
        }
        public string FileName { get; set; }
        public IRemsDbContext Context { get; set; }

        public void Create(string filename)
        {
            FileName = filename;
            var holder = new RemsDbContext(filename);

            holder.Database.EnsureCreated();
            holder.SaveChanges();

            Context = holder;
        }

        public void Open(string filename)
        {
            FileName = filename;
            Context = new RemsDbContext(filename);
        }

        public DataTable getDataTable(string name)
        {
            string text = $"SELECT * FROM {name}";
            DataTable table = null;

            using (var connection = new SqliteConnection("Data Source=" + FileName))
            {
                connection.Open();
                using (var command = new SqliteCommand(text, connection))
                using (var reader = command.ExecuteReader())
                {
                    table = new DataTable(name);
                    table.BeginLoadData();
                    table.Load(reader);
                    table.EndLoadData();
                }
            };

            return table;
        }


    }
}
