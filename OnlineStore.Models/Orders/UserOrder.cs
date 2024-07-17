using OnlineStore.Models.Base;
using OnlineStore.Models.Users;

namespace OnlineStore.Models.Orders
{
    public class UserOrder : EntityBase
    {
        public int UserId { get; set; }
        public required User User { get; set; }
        public int OrderId { get; set; }
        public required Order Order { get; set; }
    }
}
