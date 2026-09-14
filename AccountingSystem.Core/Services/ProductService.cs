using AutoMapper;
using AccountingSystem.Core.DTOs;
using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Interfaces;

namespace AccountingSystem.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, IMapper mapper, ILogger<ProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            try
            {
                var product = _mapper.Map<Product>(dto);
                var result = await _repository.AddAsync(product);
                await _repository.SaveChangesAsync();
                return _mapper.Map<ProductDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateProductAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                    return null;

                _mapper.Map(dto, product);
                product.UpdatedAt = DateTime.Now;
                var result = await _repository.UpdateAsync(product);
                await _repository.SaveChangesAsync();
                return _mapper.Map<ProductDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateProductAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteProductAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
        {
            try
            {
                var products = await _repository.GetLowStockProductsAsync();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLowStockProductsAsync: {ex.Message}");
                throw;
            }
        }
    }
}