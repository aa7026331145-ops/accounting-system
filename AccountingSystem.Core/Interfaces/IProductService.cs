using AccountingSystem.Core.Entities;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync();
    }
}