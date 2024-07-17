using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class UserOrderRequestDto: BaseDto
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
    }
}
