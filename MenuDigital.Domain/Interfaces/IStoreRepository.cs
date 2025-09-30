using MenuDigital.Domain.Entities;

namespace MenuDigital.Domain.Interfaces
{
    public interface IStoreRepository
    {
            Task<StoreModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
            Task<ICollection<StoreModel>> GetAllAsync(CancellationToken ct = default);
            Task AddAsync(StoreModel store, CancellationToken ct = default);
            Task UpdateAsync(StoreModel store, CancellationToken ct = default);
            Task DeleteAsync(StoreModel store, CancellationToken ct = default);
    }
}
