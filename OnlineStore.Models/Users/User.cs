using OnlineStore.Models.Base;
using OnlineStore.Models.Orders;

namespace OnlineStore.Models.Users
{
    public class User : EntityBase
    {
        public required string FullName { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; } = new List<UserOrder>();
    }
}
