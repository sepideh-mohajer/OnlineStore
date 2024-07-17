using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class UserOrderResponseDto : BaseDto
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
    }
}
