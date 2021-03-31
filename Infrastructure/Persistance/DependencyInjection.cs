using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common.Interfaces;


namespace Rems.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services
                .AddSingleton<IFileManager, FileManager>()
                .AddSingleton<IRemsDbContextFactory, RemsDbContextFactory>();
        }

        public class RemsDbContextFactory : IRemsDbContextFactory
        {
            private readonly IFileManager _manager;

            public RemsDbContextFactory(IFileManager manager)
            {
                _manager = manager;
            }

            public IRemsDbContext Create()
            {
                string connection = _manager.DbConnection;

                var options = new DbContextOptionsBuilder<RemsDbContext>()
                    .UseSqlite("Data Source=" + connection)
                    .UseLazyLoadingProxies(true)
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true)
                    .Options;

                return new RemsDbContext(options);
            }
        }
    }
}
