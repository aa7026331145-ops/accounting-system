using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على قائمة جميع العملاء
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customers: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// الحصول على بيانات عميل معين
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                    return NotFound(new { message = "العميل غير موجود" });

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customer {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// إنشاء عميل جديد
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerService.CreateCustomerAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating customer: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في إنشاء العميل" });
            }
        }

        /// <summary>
        /// تحديث بيانات العميل
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = await _customerService.UpdateCustomerAsync(id, dto);
                if (customer == null)
                    return NotFound(new { message = "العميل غير موجود" });

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating customer {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في تحديث البيانات" });
            }
        }

        /// <summary>
        /// حذف العميل
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(id);
                if (!result)
                    return NotFound(new { message = "العميل غير موجود" });

                return Ok(new { message = "تم حذف العميل بنجاح" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting customer {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في حذف العميل" });
            }
        }

        /// <summary>
        /// الحصول على رصيد العميل
        /// </summary>
        [HttpGet("{id}/balance")]
        public async Task<ActionResult> GetBalance(int id)
        {
            try
            {
                var balance = await _customerService.GetCustomerBalanceAsync(id);
                return Ok(new { customerId = id, balance });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting customer balance {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع الرصيد" });
            }
        }
    }
}