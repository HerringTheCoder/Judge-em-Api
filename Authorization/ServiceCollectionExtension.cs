using Microsoft.Extensions.DependencyInjection;

namespace Authorization
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuthorizationLibraryServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
