using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.MenuController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdditionalController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public AdditionalController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> CreateAddtional(AddtionalCreateDto additionalDto, Guid productId, CancellationToken ct)
        {
            ;
            var dbProd = await _appDbContext.Products.FindAsync(productId);
            if (dbProd == null)
            {
                return BadRequest("Product Id does not exist");
            }

            try
            {
                var additional = new Additional
                {
                    Id = new Guid(),
                    Name = additionalDto.Name,
                    Category = additionalDto.Category,
                    Max = additionalDto.Max,
                    Min = additionalDto.Min,
                    Size = additionalDto.Size,
                    ProductId = productId,
                    Product = dbProd,
                    ProductIdList = Additional.ToStringArray(additionalDto.ProductIdList),
                };

                await _appDbContext.AddAsync(additional, ct);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(); ;
        }
    }
    
}
