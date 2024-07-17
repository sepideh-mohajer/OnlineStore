using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using OnlineStore.Business.Interfaces;
using OnlineStore.Business.Services.Base;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Products;

namespace OnlineStore.Business.Services
{
    public class ProductService : BaseService<Product, ProductRequestDto, ProductResponseDto>
        , IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, IConfiguration configuration)
           : base(unitOfWork, mapper)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            if (_cache.TryGetValue(id, out ProductResponseDto cachedProduct))
                return cachedProduct;

            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id, p => p.ProductDiscounts);

            if (product == null)
                return null;

            decimal finalPrice = product.Price;

            if (product.ProductDiscounts != null &&
                product.ProductDiscounts.Any(p=>p.StartDate <= DateTime.Now &&
                p.EndDate >= DateTime.Now))
            {
                var currentDiscount = product.ProductDiscounts.Single(p => p.StartDate <= DateTime.Now &&
                p.EndDate >= DateTime.Now);
                finalPrice = product.Price * (1 - currentDiscount.DiscountValue / 100);
            }

            var productDto = new ProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = finalPrice,
                InventoryCount = product.InventoryCount
            };

            var cacheDuration = Convert.ToInt16(_configuration.
                GetSection("CacheSettings:ProductCacheDurationMinutes")?.Value);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(cacheDuration));

            _cache.Set(id, productDto, cacheEntryOptions);

            return productDto;
        }
    }
}
