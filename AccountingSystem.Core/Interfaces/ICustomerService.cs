using AccountingSystem.Core.Entities;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(int id);
        Task<decimal> GetCustomerBalanceAsync(int id);
    }
}