
using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigitalApi.DTOs.Menu.Products.Request.Update;
using MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu;
using MenuDigitalApi.DTOs.Transformers.Product;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ICollection<ProductGetAllReponseDto>>> GetAll(CancellationToken ct)
        {
            var result = await _productService.GetAllAsync(ct);
            var productDto = new List<ProductGetAllReponseDto>();

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
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductMenuCreate product, CancellationToken ct)
        {
            var dbProduct = ProductTransformer.Create(product, ct);
            await _productService.CreateAsync(dbProduct, ct);
            return Ok(dbProduct.ProductId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProductMenuRequestUpdateDto productDto, CancellationToken ct)
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


