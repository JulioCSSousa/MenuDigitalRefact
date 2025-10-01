using MenuDigital.Application.DTOs.Menu;
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
        public MenuController(MenuService menuService) 
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService)); ;
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
        }
        [HttpDelete]
        public async Task DeleteAsync(MenuModel menu, CancellationToken ct = default)
        {
            await _menuService.DeleteAsync(menu, ct);
        }

        [HttpGet]
        public async Task<ICollection<MenuModel>> GetAllAsync(CancellationToken ct = default)
        {
            return await _menuService.GetAllAsync(ct);  
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
        public async Task<ActionResult> UpdateAsync(Guid id, MenuUpdate menu, CancellationToken ct = default)
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
                Products = dbMenu.Products,
                MenuName = menu.MenuName,
                Active = menu.Active,
                Index = menu.index,

            };

            _menuService.UpdateAsync(menuUpdate, ct);
            return Ok(menuUpdate);
        }
    }
}
