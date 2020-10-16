using Core.Services;
using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCoreLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ISummaryService, SummaryService>();
            return services;
        }
    }
}
