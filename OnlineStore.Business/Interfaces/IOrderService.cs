using OnlineStore.Models.Dtos;

namespace OnlineStore.Business.Interfaces
{
    public interface IOrderService 
    {
        Task<OrderResponseDto> BuyAsync(OrderRequestDto requestDto);
    }
}
