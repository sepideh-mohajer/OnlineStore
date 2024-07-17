using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models.Products;

namespace OnlineStore.DataAccess.ModelConfigs
{
    public class ProductDiscountConfig : IEntityTypeConfiguration<ProductDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.ToTable("ProductDiscounts", "BaseInfo");
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
