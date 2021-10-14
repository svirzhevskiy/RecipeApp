using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.DefaultConfiguration(tableName: "Ingredients");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}