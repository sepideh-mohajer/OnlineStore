using OnlineStore.Models.Base;
using OnlineStore.Models.Products;

namespace OnlineStore.Models.Orders
{
    public class Order : EntityBase
    {
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public int OrderNo { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; } = new List<UserOrder>();
    }
}
