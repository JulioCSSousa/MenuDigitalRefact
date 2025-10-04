// Application/Services/ProductService.cs
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities;


namespace MenuDigital.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMenuRepository _menu;
        private readonly IUnitOfWork _uow;

        public ProductService() { }
        public ProductService(IProductRepository repo, IUnitOfWork uow, IMenuRepository menu)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _menu = menu;
        }

        // READ
        public virtual async Task<ICollection<ProductModel>> GetAllAsync(CancellationToken ct = default)
        {
            return await _repo.GetAllAsync(ct);
        }
            

        public virtual async Task<ICollection<ProductModel>> GetByStoreAsync(Guid storeId, CancellationToken ct = default)
        {
            return await _repo.GetByStoreAsync(storeId, ct);

        }
           

        public virtual async Task<ProductModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id, ct);
        }


        // CREATE
        public virtual async Task CreateAsync(ProductModel product, CancellationToken ct = default)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.", nameof(product));

            await _repo.AddAsync(product, ct);
            await _uow.SaveChangesAsync(ct);
        }

        // UPDATE (idempotente: retorna false se não existe)
        public virtual async Task<bool> UpdateAsync(ProductModel product, CancellationToken ct = default)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.ProductId.ToString()))
                throw new ArgumentException("Product Id is required.", nameof(product));

            var existing = await _repo.GetByIdAsync(Guid.Parse(product.ProductId.ToString()), ct);
            if (existing is null) return false;

            // exemplo de campos atualizáveis (ajuste conforme sua regra)
            existing.Name = product.Name ?? existing.Name;
            existing.Description = product.Description ?? existing.Description;
            existing.ImgUrl = product.ImgUrl ?? existing.ImgUrl;
            existing.IsSale = product.IsSale;
            existing.ExtraIndex = product.ExtraIndex;

            // preços / preview precios / combined: substitui por completo
            if (product.Prices?.Any() == true)
                existing.Prices = product.Prices;
            if (product.PreviewPrices?.Any() == true)
                existing.PreviewPrices = product.PreviewPrices;
            if (product.CombinedProducts?.Any() == true)
                existing.CombinedProducts = product.CombinedProducts;


            await _repo.UpdateAsync(existing, ct);
            await _uow.SaveChangesAsync(ct);

            return true;
        }

        // DELETE por Id (conveniente)
        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing is null) return false;

            await _repo.DeleteAsync(existing, ct);
            await _uow.SaveChangesAsync(ct);

            return true;
        }
    }
}
