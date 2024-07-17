using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class ProductRequestDto : BaseDto
    {
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public List<ProductDiscountRequestDto>? ProductDiscounts { get; set; }

        private int _inventoryCount = 100;

        public int InventoryCount
        {
            get => _inventoryCount;
            set => _inventoryCount = (value > 0) ? value : 100;
        }

    }
}
