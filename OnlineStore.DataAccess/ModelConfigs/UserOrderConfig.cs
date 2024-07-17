using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models.Orders;

namespace OnlineStore.DataAccess.ModelConfigs
{
    public class UserOrderConfig : IEntityTypeConfiguration<UserOrder>
    {
        public void Configure(EntityTypeBuilder<UserOrder> builder)
        {
            builder.ToTable("UserOrders", "Store");
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.User)
                   .WithMany(d => d.UserOrders)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Order)
                  .WithMany(d => d.UserOrders)
                  .HasForeignKey(d => d.OrderId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
