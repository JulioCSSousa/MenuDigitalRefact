// Api/Controllers/StoresController.cs
using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Store;
using MenuDigitalApi.DTOs.Transformers.Store;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.StoreControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _service;

        public StoreController(StoreService StoreService)
        {
            _service = StoreService ?? throw new ArgumentNullException(nameof(StoreService));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<StoreModel>>> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            if (result == null)
            {
                return NotFound();
            }
            return result.ToList();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var Store = await _service.GetByIdAsync(id, ct);
            return Store is null ? NotFound() : Ok(Store);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateDto Store, CancellationToken ct)
        {
            var dbStore = StoreCreateTransform.ToDbModel(Store);
            if(dbStore == null)
            {
                return BadRequest("Transform Failed and returned null");
            }
            await _service.AddAsync(dbStore, ct);
            foreach (var item in dbStore.WorkSchedule)
            {
                Console.WriteLine(item);
            }
            return Ok(dbStore);
            
        }

        [HttpPost("{storeId}/schedule")]

        public async Task<ActionResult> AddAddress(AddressModel addressModel, Guid storeId, CancellationToken ct = default)
        {
            try
            {
                await _service.AddAddressAsync(addressModel, storeId);
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
            await _service.UpdateAsync(id, Store, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }

}

