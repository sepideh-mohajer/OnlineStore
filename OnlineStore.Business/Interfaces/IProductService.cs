using OnlineStore.Models.Dtos;

namespace OnlineStore.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetProductByIdAsync(int id);
    }
}
