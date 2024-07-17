using Microsoft.AspNetCore.Mvc;
using OnlineStore.Business.Interfaces;
using OnlineStore.Infrastructure.Filters;
using OnlineStore.Models.Dtos;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("buy")]
        public async Task<ActionResult<OrderResponseDto>> Buy(OrderRequestDto requestDto)
        {
            var responseDto = await _orderService.BuyAsync(requestDto);
            return CreatedAtAction("buy", responseDto);
        }

    }
}
