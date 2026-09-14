using AccountingSystem.Core.Entities;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Core.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<InvoiceDto> UpdateInvoiceAsync(int id, CreateInvoiceDto dto);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<bool> ConfirmInvoiceAsync(int id);
        Task<decimal> GetInvoiceTotalAsync(int id);
    }
}