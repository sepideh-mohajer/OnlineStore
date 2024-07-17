using AutoMapper;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Orders;
using OnlineStore.Models.Products;
using OnlineStore.Models.Users;

namespace OnlineStore.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductResponseDto>();

            CreateMap<ProductDiscountRequestDto, ProductDiscount>();
            CreateMap<ProductDiscount, ProductDiscountResponseDto>();

            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();

            CreateMap<OrderRequestDto, Order>();
            CreateMap<Order, OrderResponseDto>();

            CreateMap<UserOrderRequestDto, UserOrder>();
            CreateMap<UserOrder, UserOrderResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(m => m.User.FullName));
        }
    }
}
