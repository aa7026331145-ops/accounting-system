using AutoMapper;
using AccountingSystem.Core.DTOs;
using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Interfaces;

namespace AccountingSystem.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCustomerByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllCustomersAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(dto);
                var result = await _repository.AddAsync(customer);
                await _repository.SaveChangesAsync();
                return _mapper.Map<CustomerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateCustomerAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto dto)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id);
                if (customer == null)
                    return null;

                _mapper.Map(dto, customer);
                customer.UpdatedAt = DateTime.Now;
                var result = await _repository.UpdateAsync(customer);
                await _repository.SaveChangesAsync();
                return _mapper.Map<CustomerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCustomerAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCustomerAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> GetCustomerBalanceAsync(int id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id);
                return customer?.Balance ?? 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCustomerBalanceAsync: {ex.Message}");
                throw;
            }
        }
    }
}