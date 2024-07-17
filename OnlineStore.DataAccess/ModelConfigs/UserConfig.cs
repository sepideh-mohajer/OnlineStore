using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Models.Users;

namespace OnlineStore.DataAccess.ModelConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "BaseInfo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FullName).HasMaxLength(100).IsRequired();
        }
    }
}
