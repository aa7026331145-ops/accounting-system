using AutoMapper;
using AccountingSystem.Core.DTOs;
using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.Enums;
using AccountingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AccountingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(AccountingDbContext dbContext, IMapper mapper, ILogger<InvoiceService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            try
            {
                var invoice = await _dbContext.Invoices
                    .Include(i => i.Items)
                    .Include(i => i.Customer)
                    .FirstOrDefaultAsync(i => i.Id == id);

                return _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetInvoiceByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            try
            {
                var invoices = await _dbContext.Invoices
                    .Include(i => i.Customer)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllInvoicesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            try
            {
                var invoice = _mapper.Map<Invoice>(dto);
                invoice.Status = InvoiceStatus.Draft;

                _dbContext.Invoices.Add(invoice);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateInvoiceAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(int id, CreateInvoiceDto dto)
        {
            try
            {
                var invoice = await _dbContext.Invoices
                    .Include(i => i.Items)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (invoice == null)
                    return null;

                _mapper.Map(dto, invoice);
                invoice.UpdatedAt = DateTime.Now;

                _dbContext.Invoices.Update(invoice);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<InvoiceDto>(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateInvoiceAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            try
            {
                var invoice = await _dbContext.Invoices.FindAsync(id);
                if (invoice == null)
                    return false;

                invoice.DeletedAt = DateTime.Now;
                _dbContext.Invoices.Update(invoice);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteInvoiceAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ConfirmInvoiceAsync(int id)
        {
            try
            {
                var invoice = await _dbContext.Invoices.FindAsync(id);
                if (invoice == null)
                    return false;

                invoice.Status = InvoiceStatus.Confirmed;
                invoice.UpdatedAt = DateTime.Now;
                _dbContext.Invoices.Update(invoice);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ConfirmInvoiceAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> GetInvoiceTotalAsync(int id)
        {
            try
            {
                var invoice = await _dbContext.Invoices.FindAsync(id);
                return invoice?.Total ?? 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetInvoiceTotalAsync: {ex.Message}");
                throw;
            }
        }
    }
}