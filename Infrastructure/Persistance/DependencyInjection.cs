using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common.Interfaces;


namespace Rems.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IRemsDbFactory, RemsDbFactory>();
            services.AddTransient(provider => provider.GetService<IRemsDbFactory>().Create());            

            return services;
        }
    }
}
