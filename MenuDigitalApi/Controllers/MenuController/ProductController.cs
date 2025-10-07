
using MenuDigital.Application.Services;
using MenuDigitalApi.DTOs.Menu.Products.Request.Update;
using MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using Microsoft.AspNetCore.Mvc;
using MenuDigitalApi.DTOs.Transformers;

namespace MenuDigitalApi.Controllers.MenuController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProductGetAllDto>>> GetAll(CancellationToken ct)
        {
            var result = await _productService.GetAllAsync(ct);
            var productDto = new List<ProductGetAllDto>();

            foreach (var item in result)
            {
                productDto.Add(ProductTransformer.GetAll(item));
            }

            return Ok(productDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("Store/{id}")]
        public async Task<ActionResult> GetByStoreId(Guid id, CancellationToken ct)
        {
            var product = await _productService.GetByStoreAsync(id, ct);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreate product, CancellationToken ct)
        {
            var dbProduct = ProductTransformer.Create(product, ct);
            try
            {
                await _productService.CreateAsync(dbProduct, ct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dbProduct.ProductId); ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProductUpdateDto productDto, CancellationToken ct)
        {
            var dbProduct = await _productService.GetByIdAsync(id);
            if (dbProduct == null)
            {
                return NotFound("Product not found");
            }

            var updated = ProductTransformer.ProductUpdateDto(productDto, dbProduct);
            await _productService.UpdateAsync(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _productService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}


