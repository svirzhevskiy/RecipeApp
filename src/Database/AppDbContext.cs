using System.Reflection;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext)));
            
            base.OnModelCreating(builder);
        }
    }
}