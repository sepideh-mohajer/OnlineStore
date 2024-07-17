using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class UserResponseDto : BaseDto
    {
        public required string FullName { get; set; }
        public List<UserOrderResponseDto>? UserOrders { get; set; }
    }
}
