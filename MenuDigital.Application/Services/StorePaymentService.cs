

using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Services
{
    public class StorePaymentService
    {
        private readonly IStorePaymentRepository _repo;
        private readonly IUnitOfWork _uow;
        public StorePaymentService(IStorePaymentRepository repo, IUnitOfWork uow) {

            _repo = repo;
            _uow = uow;
        }

        public async Task AddAsync(StorePayments storePayments, CancellationToken ct = default)
        {
            await _repo.AddAsync(storePayments);
            await _uow.SaveChangesAsync();
        }

        public async Task<StorePayments> GetByIdAsync(long id)
        {
            return await _repo.GetByIdAsync(id);

        }

        public Task DeleteAsync(long id)
        {
            var dbPayment = GetByIdAsync(id);
            if (dbPayment != null)
            {
                _repo.DeleteAsync(id);
                _uow.SaveChangesAsync();
                return Task.CompletedTask;
            }
            return null;
        }

        public async Task<ICollection<StorePayments>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task UpdateAsync(long paymentId, StorePayments addpayment, CancellationToken ct = default)
        {
            if (addpayment != null || paymentId != null)
            {
                await _repo.UpdateAsync(paymentId, addpayment, ct);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
