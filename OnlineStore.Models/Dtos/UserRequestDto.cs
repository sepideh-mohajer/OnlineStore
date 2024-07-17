using OnlineStore.Models.Base;

namespace OnlineStore.Models.Dtos
{
    public class UserRequestDto: BaseDto
    {
        public required string FullName { get; set; }
    }
}
