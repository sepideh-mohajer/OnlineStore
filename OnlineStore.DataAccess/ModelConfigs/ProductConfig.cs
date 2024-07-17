using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models.Products;

namespace OnlineStore.DataAccess.ModelConfigs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "BaseInfo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).HasMaxLength(40).IsRequired();

            builder.HasIndex(e => e.Title)
               .IsUnique();

        }
    }
}
