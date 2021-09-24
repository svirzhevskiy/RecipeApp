using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Extensions
{
    public static class EntityConfigurationExtension
    {
        public static EntityTypeBuilder<T> DefaultConfiguration<T>(
            this EntityTypeBuilder<T> builder, 
            string tableName)
            where T : BaseEntity
        {
            builder.ToTable(tableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            return builder;
        }
    }
}