// Application/Services/StoreService.cs
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Services
{
    public class StoreService
    {
        private readonly IStoreRepository _repo;
        private readonly IUnitOfWork _uow;

        public StoreService(IStoreRepository repo, IUnitOfWork uow)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        // READ
        public async Task<ICollection<StoreModel>> GetAllAsync(string? name, string? url, CancellationToken ct = default)
        {
            var result = await _repo.GetAllAsync(ct);

            var query = result.AsEnumerable();

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(n => n.StoreName.ToLower().Contains(name.ToLower()));
            }
            if (!String.IsNullOrEmpty(url))
            {
                query = query.Where(u => u.StoreUrl.ToLower().Contains(url.ToLower()));
            }
            return query.ToList();
        }

        public async Task<StoreModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _repo.GetByIdAsync(id, ct);
        }

        // CREATE
        public async Task AddAsync(StoreModel s, CancellationToken ct = default)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));

            await _repo.AddAsync(s, ct);
            await _uow.SaveChangesAsync(ct);
        }

        public async Task AddAddressAsync(AddressModel address, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
            if(dbStore.Address.Count == 0 || dbStore.Address is null)
            {
                dbStore.Address.Add(address);
                await _uow.SaveChangesAsync(ct);
            }
        }
        public async Task AddWorkScheduleAsync(WorkSchedule work, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
                dbStore.WorkSchedule.Add(work);
                await _uow.SaveChangesAsync(ct);
        }
        public async Task AddStorePaymentAsync(StorePayments addpayment, Guid storeId, CancellationToken ct = default)
        {
            var dbStore = await _repo.GetByIdAsync(storeId, ct);
            if (dbStore is null) throw new ArgumentNullException("StoreId not Found");
                dbStore.StorePayments.Add(addpayment);
                await _uow.SaveChangesAsync(ct);
        }

        // UPDATE (merge seguro + validação)
        public async Task<bool> UpdateAsync(StoreModel incoming, CancellationToken ct = default)
        {

            await _repo.UpdateAsync(incoming);
            await _uow.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var prod = await GetByIdAsync(id);
            if (prod != null)
            {
                await _repo.DeleteAsync(prod, ct);
                await _uow.SaveChangesAsync(ct);
                return true;
            }
            return false;

        }

    }

}

