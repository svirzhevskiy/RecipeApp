using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.DefaultConfiguration(tableName: "Recipes");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Instructions)
                .IsRequired();

            builder
                .HasMany(x => x.Ingredients)
                .WithMany(y => y.Recipes);

            builder
                .HasOne(x => x.Image);
        }
    }
}