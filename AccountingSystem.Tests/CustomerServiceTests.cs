using Xunit;
using Moq;
using AccountingSystem.Core.Services;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.DTOs;
using AutoMapper;

namespace AccountingSystem.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CustomerService>> _mockLogger;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CustomerService>>();
            _service = new CustomerService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WithValidId_ReturnsCustomerDto()
        {
            // Arrange
            int customerId = 1;
            var customerDto = new CustomerDto { Id = customerId, Name = "Test Customer" };
            _mockRepository.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(new AccountingSystem.Core.Entities.Customer { Id = customerId });
            _mockMapper.Setup(m => m.Map<CustomerDto>(It.IsAny<AccountingSystem.Core.Entities.Customer>())).Returns(customerDto);

            // Act
            var result = await _service.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            _mockRepository.Verify(r => r.GetByIdAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task CreateCustomerAsync_WithValidDto_ReturnsCreatedCustomerDto()
        {
            // Arrange
            var createDto = new CreateCustomerDto { Name = "New Customer", Code = "C001" };
            var customer = new AccountingSystem.Core.Entities.Customer { Id = 1, Name = "New Customer" };
            var customerDto = new CustomerDto { Id = 1, Name = "New Customer" };

            _mockMapper.Setup(m => m.Map<AccountingSystem.Core.Entities.Customer>(createDto)).Returns(customer);
            _mockRepository.Setup(r => r.AddAsync(customer)).ReturnsAsync(customer);
            _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = await _service.CreateCustomerAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Customer", result.Name);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<AccountingSystem.Core.Entities.Customer>()), Times.Once);
        }
    }
}