// Api/Controllers/StoresController.cs
using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.StoreControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _StoreService;

        public StoreController(StoreService StoreService)
        {
            _StoreService = StoreService ?? throw new ArgumentNullException(nameof(StoreService));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<StoreModel>>> GetAll(CancellationToken ct)
        {
            var result = await _StoreService.GetAllAsync(ct);
            if (result == null)
            {
                return NotFound();
            }
            return result.ToList();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var Store = await _StoreService.GetByIdAsync(id, ct);
            return Store is null ? NotFound() : Ok(Store);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreModel Store, CancellationToken ct)
        {
            await _StoreService.AddAsync(Store, ct);
            return CreatedAtAction(nameof(GetById), new { id = Store.StoreId }, Store);
        }

        [HttpPost("{storeId}/schedule")]

        public async Task<ActionResult> AddAddress(AddressModel addressModel, Guid storeId, CancellationToken ct = default)
        {
            try
            {
                await _StoreService.AddAddressAsync(addressModel, storeId);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex);
            }
            return Ok("Address successfully saved");

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StoreModel Store, CancellationToken ct)
        {
            await _StoreService.UpdateAsync(id, Store, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _StoreService.DeleteAsync(id, ct);
            return NoContent();
        }
    }

}

