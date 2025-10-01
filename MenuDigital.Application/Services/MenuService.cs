using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities.MenuModels;

namespace MenuDigital.Application.Services
{
    public class MenuService
    {
        private readonly IMenuRepository _repo ;
        private readonly IUnitOfWork _uow;
        public MenuService(IMenuRepository repo, IUnitOfWork uow) 
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task AddAsync(MenuModel menu, CancellationToken ct = default)
        {
            await _repo.AddAsync(menu);
        }

        public async Task DeleteAsync(MenuModel menu, CancellationToken ct = default)
        {
           await _repo.DeleteAsync(menu);
        }

        public async Task<ICollection<MenuModel>> GetAllAsync(CancellationToken ct = default)
        {
            return await _repo.GetAllAsync();
        }

        public async Task<MenuModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<MenuModel> GetByStoreAsync(Guid storeId, CancellationToken ct = default)
        {
            return await _repo.GetByStoreAsync(storeId);
        }

        public Task UpdateAsync(MenuModel menu, CancellationToken ct = default)
        {
            _repo.UpdateAsync(menu);
            return Task.CompletedTask;
        }
    }
}
