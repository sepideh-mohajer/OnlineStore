using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class ProductDiscountRequestDto : BaseDto
    {
        public int ProductId { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
