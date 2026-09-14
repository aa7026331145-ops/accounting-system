using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Core.Interfaces;
using AccountingSystem.Core.DTOs;

namespace AccountingSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// الحصول على قائمة جميع المنتجات
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting products: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// الحصول على بيانات منتج معين
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(new { message = "المنتج غير موجود" });

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting product {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }

        /// <summary>
        /// إنشاء منتج جديد
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _productService.CreateProductAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في إنشاء المنتج" });
            }
        }

        /// <summary>
        /// تحديث بيانات المنتج
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _productService.UpdateProductAsync(id, dto);
                if (product == null)
                    return NotFound(new { message = "المنتج غير موجود" });

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في تحديث البيانات" });
            }
        }

        /// <summary>
        /// حذف المنتج
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result)
                    return NotFound(new { message = "المنتج غير موجود" });

                return Ok(new { message = "تم حذف المنتج بنجاح" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product {id}: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في حذف المنتج" });
            }
        }

        /// <summary>
        /// الحصول على المنتجات منخفضة المخزون
        /// </summary>
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStock()
        {
            try
            {
                var products = await _productService.GetLowStockProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting low stock products: {ex.Message}");
                return StatusCode(500, new { message = "حدث خطأ في استرجاع البيانات" });
            }
        }
    }
}