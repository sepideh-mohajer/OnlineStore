using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class OrderResponseDto : BaseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<UserOrderResponseDto>? UserOrders { get; set; }
    }
}
