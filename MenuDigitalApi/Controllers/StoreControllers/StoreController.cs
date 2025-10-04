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
        public async Task<ActionResult<ICollection<StoreModel>>> GetAll(string? name, string? url, CancellationToken ct)
        {
            var result = await _service.GetAllAsync(name, url, ct);
            return result.ToList();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var store = await _service.GetByIdAsync(id, ct);
            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateDto StoreDto, CancellationToken ct)
        {
            var dbStore = StoreCreateTransform.ToDbModel(StoreDto);
            if(dbStore == null)
            {
                return BadRequest("Transform Failed and returned null");
            }
            await _service.AddAsync(dbStore, ct);
            return Ok(dbStore.StoreId);
            
        }

        [HttpPost("{storeId}/address")]

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
            return NoContent();

        }
       /* [HttpPost("{storeId}/workschedule")]

        public async Task<ActionResult> AddWorkSchedule(WorkScheduleCreate work, Guid storeId, CancellationToken ct = default)
        {
            var workschedule = new WorkSchedule
            {
                Day = work.Day,
                Start = TimeSpan.Parse(work.Start),
                End = TimeSpan.Parse(work.End),
                IsSelected = work.IsSelected

            };
            try
            {
                await _service.AddWorkScheduleAsync(workschedule, storeId);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex);
            }
            return Ok("WorkSchedule successfully saved");

        }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StoreUpdateDto storeDto, CancellationToken ct)
        {
            var storeDb = await _service.GetByIdAsync(id);
            if(storeDb == null)
            {
                return BadRequest("Store not found");
            }

            StoreUpdateTransformer.ApplyUpdate(storeDb, storeDto);
            await _service.UpdateAsync(id, storeDb, ct);
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

