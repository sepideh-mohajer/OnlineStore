using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class ProductDiscountResponseDto : BaseDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
