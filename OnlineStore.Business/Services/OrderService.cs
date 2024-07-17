using AutoMapper;
using OnlineStore.Business.Interfaces;
using OnlineStore.Business.Services.Base;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Orders;
using OnlineStore.Models.Products;

namespace OnlineStore.Business.Services
{
    public class OrderService : BaseService<Order, OrderRequestDto, OrderResponseDto>
        , IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository) 
            : base(unitOfWork, mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderResponseDto> BuyAsync(OrderRequestDto requestDto)
        {
            await UpdateProuctInventoryCount(requestDto);
            Order createdEntity = await AddOrder(requestDto);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponseDto>(createdEntity);
        }

        #region Private Methods
        private async Task<Order> AddOrder(OrderRequestDto requestDto)
        {
            var order = _mapper.Map<Order>(requestDto);
            order.OrderNo = await _orderRepository.GetMaxOrderNumber();
            var createdOrder = await _unitOfWork.Repository<Order>().AddAsync(order);

            return createdOrder;
        }

        private async Task UpdateProuctInventoryCount(OrderRequestDto requestDto)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(requestDto.ProductId);
            product.InventoryCount -= requestDto.Quantity;
            if (product.InventoryCount <= 0)
                throw new Exception(Messages.ProductInventoryCountIsNotValid);

            await _unitOfWork.Repository<Product>().UpdateAsync(product);
        }
        #endregion
    }
}
