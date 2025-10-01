// Domain/Interfaces/IProductRepository.cs

// Domain/Interfaces/IProductRepository.cs
using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Interfaces.Menu;
public interface IProductRepository
{
    Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ICollection<ProductModel>> GetByStoreAsync(Guid storeId, CancellationToken ct = default);
    Task<ICollection<ProductModel>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(ProductModel product, CancellationToken ct = default);
    Task UpdateAsync(ProductModel product, CancellationToken ct = default);
    Task DeleteAsync(ProductModel product, CancellationToken ct = default);
}
