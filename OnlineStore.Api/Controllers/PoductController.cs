using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Controllers.Base;
using OnlineStore.Business.Interfaces;
using OnlineStore.Business.Interfaces.Base;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Products;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CrudBaseController<Product, ProductRequestDto, ProductResponseDto>
    {
        private readonly IProductService _productService;
        public ProductController(IBaseService<Product, ProductRequestDto, ProductResponseDto> service, 
            IProductService productService)
            : base(service)
        {
            _productService = productService;
        }

        [HttpGet("get-with-cache/{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetByIdWithCache(int id)
        {
            var entity = await _productService.GetProductByIdAsync(id);

            return Ok(entity);
        }

    }
}
