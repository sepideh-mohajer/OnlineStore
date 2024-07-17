using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class ProductResponseDto: BaseDto
    {
        public required string Title { get; set; }
        public int InventoryCount { get; set; }
        public decimal Price { get; set; }
        public List<ProductDiscountResponseDto>? ProductDiscounts { get; set; }
    }
}
