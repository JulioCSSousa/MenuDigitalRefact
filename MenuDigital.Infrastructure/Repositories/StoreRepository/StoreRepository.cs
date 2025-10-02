using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Infrastructure.Context.Repositories;

public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext _context;

    public StoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StoreModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.StoreModels
            .Include(s => s.Category)
            .Include(s => s.WorkSchedule)
            .Include(s => s.Address)
            .Include(s => s.StorePayments)
            .FirstOrDefaultAsync(p => p.StoreId == id, ct);
    }

    public async Task<ICollection<StoreModel>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.StoreModels
            .Include(s => s.Category)
            .Include(s => s.WorkSchedule)
            .Include(s => s.Address)
            .Include(s => s.StorePayments)
            .ToListAsync(ct);
    }

    public async Task AddAsync(StoreModel Store, CancellationToken ct = default)
    {
        await _context.StoreModels.AddAsync(Store, ct);
    }


    public Task UpdateAsync(StoreModel Store, CancellationToken ct = default)
    {
        _context.StoreModels.Update(Store);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(StoreModel Store, CancellationToken ct = default)
    {
        _context.StoreModels.Remove(Store);
        return Task.CompletedTask;
    }

    public async Task AddAddressAsync(Guid id, AddressModel address, CancellationToken ct = default)
    {
        var dbStore = await GetByIdAsync(id);
        dbStore.Address.Add(address);
    }
    public async Task AddWorkScheduleAsync(Guid id, WorkSchedule work, CancellationToken ct = default)
    {
        var dbStore = await GetByIdAsync(id);
        dbStore.WorkSchedule.Add(work);
    }
}
