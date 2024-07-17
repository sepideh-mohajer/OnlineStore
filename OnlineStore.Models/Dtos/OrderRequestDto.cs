using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class OrderRequestDto: BaseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<UserOrderRequestDto>? UserOrders { get; set; }

    }
}
