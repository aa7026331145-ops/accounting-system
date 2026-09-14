using AccountingSystem.Core.Entities;

namespace AccountingSystem.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByCodeAsync(string code);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetLowStockProductsAsync();
    }
}