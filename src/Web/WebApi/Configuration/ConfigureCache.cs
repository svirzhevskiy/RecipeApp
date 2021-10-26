using Application.Services;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace WebApi.Configuration
{
    public static class ConfigureCache
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.Configure<CacheSettings>(configuration.GetSection(nameof(CacheSettings)));
            services.AddScoped<ICacheService, InMemoryCacheService>();

            return services;
        }
    }
}