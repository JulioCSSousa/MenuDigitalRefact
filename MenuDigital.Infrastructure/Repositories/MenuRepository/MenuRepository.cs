using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MenuModel menu, CancellationToken ct = default)
    {
        await _context.AddAsync(menu);
    }

    public async Task DeleteAsync(MenuModel menu, CancellationToken ct = default)
    {
        _context.Remove(menu);
    }

    public async Task<ICollection<MenuModel>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Menu.ToListAsync();
    }

    public async Task<MenuModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
       return await _context.Menu.FirstOrDefaultAsync(m => m.MenuId == id);
    }

    public async Task<MenuModel> GetByStoreAsync(Guid storeId, CancellationToken ct = default)
    {
        return await _context.Menu.FirstOrDefaultAsync(m => m.StoreId == storeId);
    }

    public Task UpdateAsync(MenuModel menu, CancellationToken ct = default)
    {
        _context.Menu.Update(menu);
        return Task.CompletedTask;
    }
}
