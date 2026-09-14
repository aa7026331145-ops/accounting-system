using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AccountingDbContext _context;

        public ProductRepository(AccountingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByCodeAsync(string code)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            return await _context.Products
                .Where(p => p.Quantity <= p.MinimumQuantity && p.IsActive)
                .ToListAsync();
        }
    }
}