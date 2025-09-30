using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuDigitalApi.Controllers.StoreControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly StoreService _storeService;

        public AddressController(AppDbContext context, StoreService storeService)
        {
            _context = context;
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<AddressModel>>> GetAllAddresses()
        {
            var dbAddress = await _context.Addresses.ToListAsync();
            return Ok(dbAddress);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressModel>> GetAddressById(Guid id)
        {
            var dbAddress = _context.Addresses.FirstOrDefault(i => i.AddressId == id);
            if (dbAddress == null)
            {
                return NotFound();
            }
            return dbAddress;
        }
        [HttpPost("{storeId}")]

        public async Task<ActionResult> AddAddress(AddressModel addressModel, Guid storeId, CancellationToken ct = default)
        {
            try
            {
                await _storeService.AddAddressAsync(addressModel, storeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Address successfully saved");

        }
        [HttpPut]
        public async Task<ActionResult<AddressModel>> UpdateAddresses(Guid id, AddressModel model)
        {
            var dbAddress = await GetAddressById(id);
            if (dbAddress != null)
            {
                var updateAddress = new AddressModel
                {
                    Street = string.IsNullOrEmpty(model.Street) ? dbAddress.Value.Street : model.Street,
                    Number = string.IsNullOrEmpty(model.Number) ? dbAddress.Value.Number : model.Number,
                    neighborhood = string.IsNullOrEmpty(model.neighborhood) ? dbAddress.Value.neighborhood : model.neighborhood,
                    City = string.IsNullOrEmpty(model.City) ? dbAddress.Value.City : model.City,
                    Complement = string.IsNullOrEmpty(model.Complement) ? dbAddress.Value.Complement : model.Complement,
                    ZipCode = string.IsNullOrEmpty(model.ZipCode) ? dbAddress.Value.ZipCode : model.ZipCode,
                };

                await _context.SaveChangesAsync();
                return Ok(updateAddress);
            }
            return NotFound(dbAddress);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var dbAddress = await GetAddressById(id);
            if(dbAddress == null)
            {
                return NotFound(dbAddress);
            }
             _context.Remove(dbAddress);
            await _context.SaveChangesAsync();
            return Ok(dbAddress);
        }
    }
}
