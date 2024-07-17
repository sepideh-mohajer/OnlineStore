using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineStore.Business.Services;
using OnlineStore.Business.Services.Base;
using OnlineStore.DataAccess.Interfaces;
using OnlineStore.Models.Dtos;
using OnlineStore.Models.Products;

namespace OnlineStore.UnitTest
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;
        private static Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private static Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private static Mock<IMemoryCache> _memoryCacheMock = new Mock<IMemoryCache>();
        private static Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        private BaseService<Product, ProductRequestDto, ProductResponseDto> _baseService;

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService(_unitOfWorkMock.Object, _mapperMock.Object,
                _memoryCacheMock.Object, _configurationMock.Object);
            _baseService = new BaseService<Product, ProductRequestDto, ProductResponseDto>
                (_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetProductById_ValidId_ReturnsProduct()
        {
            // Arrange
            var productDiscount = new ProductDiscountRequestDto()
            {
                DiscountValue = (decimal)(0.2),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                ProductId = 1
            };
            var productDiscounts = new List<ProductDiscountRequestDto>();
            productDiscounts.Add(productDiscount);
            var product = new ProductRequestDto
            {
                Id = 1,
                Title = "Product Title",
                InventoryCount = 10,
                Price = 100,
                ProductDiscounts = productDiscounts
            };

            await _productService.AddAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(product.Id, result.Id);
        }

        [Test]
        public async Task GetProductPriceById_ValidId_ReturnsCorrectPrice()
        {
            // Arrange
            var productDiscount = new ProductDiscountRequestDto()
            {
                DiscountValue = (decimal)(0.2),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                ProductId = 1
            };
            var productDiscounts = new List<ProductDiscountRequestDto>();
            productDiscounts.Add(productDiscount);
            var product = new ProductRequestDto
            {
                Id = 1,
                Title = "Product Title",
                InventoryCount = 10,
                Price = 200,
                ProductDiscounts = productDiscounts
            };

            await _productService.AddAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.AreEqual(160, result.Price); // 200 - 20% = 160
        }

        [Test]
        public async Task GetProductById_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            var productDiscount = new ProductDiscountRequestDto()
            {
                DiscountValue = (decimal)(0.2),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                ProductId = 1
            };
            var productDiscounts = new List<ProductDiscountRequestDto>();
            productDiscounts.Add(productDiscount);
            var product = new ProductRequestDto
            {
                Id = 1,
                Title = "Product Title",
                InventoryCount = 10,
                Price = 200,
                ProductDiscounts = productDiscounts
            };

            await _productService.AddAsync(product);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _productService.GetByIdAsync(999));
            Assert.That(ex.Message, Is.EqualTo("Product not found"));
        }

    }
}