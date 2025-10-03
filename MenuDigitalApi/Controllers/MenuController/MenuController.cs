using MenuDigital.Application.DTOs.Menu;
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigitalApi.DTOs.Menu;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.MenuController
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;
        private readonly IUnitOfWork _uow;
        public MenuController(MenuService menuService, IUnitOfWork uow) 
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        [HttpPost]
        public async Task AddAsync(MenuCreate menu, CancellationToken ct = default)
        {
            var dbMenu = new MenuModel
            {
                StoreId = menu.StoreId,
                Active = menu.Active,
                Index = menu.Index,
                MenuName = menu.MenuName
            };
            await _menuService.AddAsync(dbMenu, ct);
            await _uow.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var result = await _menuService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            await _menuService.DeleteAsync(result, ct);
            return Ok("Successfully Deleted");
        }

        [HttpGet]
        public async Task<ICollection<MenuGetDto>> GetAllAsync(CancellationToken ct = default)
        {
            var dbModel = await _menuService.GetAllAsync(ct);
            var dto = dbModel.Select(menu => new MenuGetDto
            {
                MenuId = menu.MenuId,
                Active = menu.Active,
                Index = menu.Index,
                MenuName = menu.MenuName,
                StoreId = menu.StoreId,
                ProductIds = menu.ProductIds
            }
            ).ToList();
            return dto;
        }

        [HttpGet("{id}")]
        public async Task<MenuModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
           return await _menuService.GetByIdAsync(id, ct);
        }

        [HttpGet("store/{storeId}")]
        public async Task<MenuModel> GetByStoreAsync(Guid storeId, CancellationToken ct = default)
        {
            return await _menuService.GetByStoreAsync(storeId, ct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, Update menu, CancellationToken ct = default)
        {
            var dbMenu = await GetByIdAsync(id);
            if(dbMenu != null)
            {
                return NotFound();
            }
            var menuUpdate = new MenuModel
            {
                MenuId = dbMenu.MenuId,
                StoreId = dbMenu.StoreId,
                ProductIds = dbMenu.ProductIds,
                MenuName = menu.MenuName,
                Active = menu.Active,
                Index = menu.index,

            };

            _menuService.UpdateAsync(menuUpdate, ct);
            await _uow.SaveChangesAsync();
            return Ok(menuUpdate);
        }
    }
}
