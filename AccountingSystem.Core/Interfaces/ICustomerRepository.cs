using AccountingSystem.Core.Entities;

namespace AccountingSystem.Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetByCodeAsync(string code);
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    }
}