using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common.Interfaces;


namespace Rems.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {

            //services.AddDbContext<RemsDbContext>(options =>
            //options.UseSqlite("Data Source=Data\\Database.db"));//  $"Data Source={file};");

            services.AddScoped<IRemsDbFactory, RemsDbFactory>(); // (provider => provider.GetService<RemsDbFactory>());

            return services;
        }
        public static IServiceCollection AddWebPersistence(this IServiceCollection services)
        {
            
            services.AddDbContext<RemsDbContext>(options =>
            options.UseSqlite("Data Source=Data\\Database.db"));//  $"Data Source={file};");
            services.AddScoped<IRemsDbContext>(provider => provider.GetService<RemsDbContext>());

            services.AddScoped<IRemsDbFactory, RemsDbFactory>();// (provider => new RemsDbFactory() { FileName = "Data\\Database.db" });

            return services;
        }
    }
}
