using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Storage
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddStorageLibraryServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<JudgeContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });
            return services;
        }
    }
}
