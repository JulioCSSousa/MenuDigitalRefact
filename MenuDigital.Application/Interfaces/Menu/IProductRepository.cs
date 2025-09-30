using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Interfaces.Menu
{
    internal interface IProductRepository
    {
        Task<ProductModel?> GetByIdAsync(string id);
        Task<IEnumerable<ProductModel>> GetAllAsync();
        Task AddAsync(ProductModel product);
        Task UpdateAsync(string id, ProductModel product);
        Task DeleteAsync(string id);

    }
}
