using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IInvoiceService invoiceService, ILogger<InvoicesController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على قائمة جميع الفواتير
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAll()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoicesAsync();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting invoices: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// الحصول على بيانات فاتورة معينة
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
                if (invoice == null)
                    return NotFound(new { message = "الفاتورة غير موجودة" });

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting invoice {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// إنشاء فاتورة جديدة
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var invoice = await _invoiceService.CreateInvoiceAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating invoice: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في إنشاء الفاتورة" });
            }
        }

        /// <summary>
        /// تحديث الفاتورة
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceDto>> Update(int id, [FromBody] CreateInvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var invoice = await _invoiceService.UpdateInvoiceAsync(id, dto);
                if (invoice == null)
                    return NotFound(new { message = "الفاتورة غير موجودة" });

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating invoice {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في تحديث البيانات" });
            }
        }

        /// <summary>
        /// حذف الفاتورة
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _invoiceService.DeleteInvoiceAsync(id);
                if (!result)
                    return NotFound(new { message = "الفاتورة غير موجودة" });

                return Ok(new { message = "تم حذف الفاتورة بنجاح" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting invoice {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في حذف الفاتورة" });
            }
        }

        /// <summary>
        /// تأكيد الفاتورة
        /// </summary>
        [HttpPost("{id}/confirm")]
        public async Task<ActionResult> Confirm(int id)
        {
            try
            {
                var result = await _invoiceService.ConfirmInvoiceAsync(id);
                if (!result)
                    return NotFound(new { message = "الفاتورة غير موجودة" });

                return Ok(new { message = "تم تأكيد الفاتورة بنجاح" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error confirming invoice {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في تأكيد الفاتورة" });
            }
        }

        /// <summary>
        /// الحصول على إجمالي الفاتورة
        /// </summary>
        [HttpGet("{id}/total")]
        public async Task<ActionResult> GetTotal(int id)
        {
            try
            {
                var total = await _invoiceService.GetInvoiceTotalAsync(id);
                return Ok(new { invoiceId = id, total });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting invoice total {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في الحساب" });
            }
        }
    }
}