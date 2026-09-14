using Xunit;
using Moq;
using AccountingSystem.Core.Services;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.DTOs;
using AutoMapper;

namespace AccountingSystem.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _service = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ReturnsProductDto()
        {
            // Arrange
            int productId = 1;
            var productDto = new ProductDto { Id = productId, Name = "Test Product" };
            _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(new AccountingSystem.Core.Entities.Product { Id = productId });
            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<AccountingSystem.Core.Entities.Product>())).Returns(productDto);

            // Act
            var result = await _service.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task GetLowStockProductsAsync_ReturnsList()
        {
            // Arrange
            var products = new List<AccountingSystem.Core.Entities.Product>();
            var productDtos = new List<ProductDto>();
            _mockRepository.Setup(r => r.GetLowStockProductsAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act
            var result = await _service.GetLowStockProductsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}