using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) => _context = context;

    public async Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var sid = id;
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Additional)
            .FirstOrDefaultAsync(p => p.ProductId == sid, ct);
    }

    public async Task<ICollection<ProductModel>> GetByStoreAsync(Guid storeId, CancellationToken ct = default)
    {

        return await _context.Products
            .AsNoTracking()
            .Where(p => p.StoreId == storeId)
            .Include(p => p.Additional)
            .ToListAsync(ct);
    }

    public async Task<ICollection<ProductModel>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Products
        .Include(p => p.Additional)
        .AsNoTracking()
        .ToListAsync(ct);
    }

    public async Task AddAsync(ProductModel product, CancellationToken ct = default)
    {
        await _context.Products.AddAsync(product, ct);
    }

    public Task UpdateAsync(ProductModel product, CancellationToken ct = default)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProductModel product, CancellationToken ct = default)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }
}
