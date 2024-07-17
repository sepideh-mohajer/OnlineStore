using OnlineStore.Models.Base;

namespace OnlineStore.Models.Products
{
    public class ProductDiscount : EntityBase
    {
        public int ProductId { get; set; }
        public required Product Product { get; set; } 
        public decimal DiscountValue { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
