using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AccountingDbContext _context;

        public CustomerRepository(AccountingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer> GetByCodeAsync(string code)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
        {
            return await _context.Customers.Where(c => c.IsActive).ToListAsync();
        }
    }
}