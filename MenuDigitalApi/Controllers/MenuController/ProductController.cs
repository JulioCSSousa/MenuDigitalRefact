
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
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductMenuCreate product, CancellationToken ct)
        {
            var category = new List<Category>();
            foreach (var item in product.Category)
            {
                category.Add(new Category
                {
                    Name = item.Name,
                    Description = item.Description
                }
                );

            }
            var combined = new List<CombinedProduct>();
            if (product.CombinedProducts.Count != 0 || product.CombinedProducts != null)
            {
                foreach (var item in product.CombinedProducts)
                {
                    combined.Add(new CombinedProduct
                    {
                        Name = item.Name,
                        MainMenu = item.MainMenu,
                        Category = item.Category,
                        Max = item.Max,
                        Min = item.Min,
                        Prices = item.Prices,
                        Size = item.Size,
                        Type = item.Type,
                    }
                    );

                }
            }

            var dbProduct = new ProductModel
            {
                ProductId = product.ProductId,
                Category = category,
                Name = product.Name,
                CombinedPrice = product.CombinedPrice,
                Description = product?.Description,
                ExtraIndex = product?.ExtraIndex,
                ImgUrl = product?.ImgUrl,
                IsSale = product.IsSale,
                Multiple = product.Multiple,
                Observations = product.Observations,
                PreviewPrices = product.PreviewPrices,
                Prices = product.Prices,
                CombinedProducts = combined,

            };
            await _productService.CreateAsync(dbProduct, ct);
            return Ok(dbProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProductMenuRequestUpdateDto productDto, CancellationToken ct)
        {
            var dbProduct = await _productService.GetByIdAsync(id);
            if (dbProduct == null)
            {
                return NotFound();
            }

            var updated = ProductTransformer.ProductUpdateDto(productDto, dbProduct);
            await _productService.UpdateAsync(updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _productService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}


