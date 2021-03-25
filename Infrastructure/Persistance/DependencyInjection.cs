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
                .AddTransient<RemsDbContextFactory>()
                .AddScoped<IRemsDbContext>(provider => provider.GetRequiredService<RemsDbContextFactory>().Create());
        }

        public class RemsDbContextFactory
        {
            private readonly IFileManager _manager;

            public RemsDbContextFactory(IFileManager manager)
            {
                _manager = manager;
            }

            public RemsDbContext Create()
            {
                string connection = _manager.DbConnection;

                var builder = new DbContextOptionsBuilder<RemsDbContext>()
                    .UseSqlite("Data Source=" + connection)
                    .UseLazyLoadingProxies(true)
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true);

                return new RemsDbContext(builder.Options);
            }
        }
    }
}
