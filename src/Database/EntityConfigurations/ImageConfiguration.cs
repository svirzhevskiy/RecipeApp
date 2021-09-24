using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.DefaultConfiguration(tableName: "Images");

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(300);
            
            builder.Property(x => x.Extension)
                .HasMaxLength(5);
        }
    }
}