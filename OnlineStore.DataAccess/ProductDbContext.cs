using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.ModelConfigs;

namespace OnlineStore.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            SetModelConfigs(builder);
        }

        private void SetModelConfigs(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new UserOrderConfig());
        }
    }
}
