using System.Reflection;
using Application.Behaviors;
using Application.Features.RecipeFeatures.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class ConfigureMediatr
    {
        public static IServiceCollection AddApplicationMediatr(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(GetAllRecipesQuery)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            return services;
        }
    }
}