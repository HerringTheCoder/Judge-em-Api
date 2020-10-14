using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCoreLibraryServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
