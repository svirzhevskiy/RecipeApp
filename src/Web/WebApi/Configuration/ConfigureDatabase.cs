using Application.Repositories;
using Database;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Configuration
{
    public static class ConfigureDatabase
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            var connectionString = configuration.GetConnectionString("localPostgres");
            services.AddDbContextPool<AppDbContext>(op =>
            {
                op.UseNpgsql(connectionString);
            });
            
            return services;
        }
    }
}