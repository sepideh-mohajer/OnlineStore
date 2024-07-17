
using OnlineStore.Models.Base;
using OnlineStore.Models.Dtos;

namespace OnlineStore.Models.Products
{
    public class Product : EntityBase
    {
        public required string Title { get; set; }
        public int InventoryCount { get; set; }
        public decimal Price { get; set; }
        public List<ProductDiscount>? ProductDiscounts { get; set; }
    }
}
