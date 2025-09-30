using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.StoreControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorePaymentController : Controller
    {
        private readonly StorePaymentService _service;

        public StorePaymentController(StorePaymentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(StorePayments payments)
        {
            try
            {
                await _service.AddAsync(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(long id, StorePayments payments)
        {
            try
            {
                await _service.UpdateAsync(id, payments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<StorePayments>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            if (result == null)
            {
                return NotFound();
            }
            return result.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StorePayments>> GetByIdAsync(long id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
