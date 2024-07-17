using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models.Orders;

namespace OnlineStore.DataAccess.ModelConfigs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Store");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.OrderNo).HasMaxLength(100).IsRequired();

            builder.HasOne(d => d.Product)
                   .WithMany()
                   .HasForeignKey(d => d.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
