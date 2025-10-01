using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.Interfaces.Menu
{
    public interface IMenuRepository
    {
        Task<MenuModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<MenuModel> GetByStoreAsync(Guid storeId, CancellationToken ct = default);
        Task<ICollection<MenuModel>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(MenuModel menu, CancellationToken ct = default);
        Task UpdateAsync(MenuModel menu, CancellationToken ct = default);
        Task DeleteAsync(MenuModel menu, CancellationToken ct = default);
    }
}
