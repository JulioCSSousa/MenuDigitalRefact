using MenuDigital.Domain.Entities;

namespace MenuDigital.Application.Interfaces.Store
{
    public interface IStorePaymentRepository
    {
        public Task AddAsync(StorePayments storePayments, CancellationToken ct = default);
        public Task UpdateAsync(long paymentId, StorePayments addpayment, CancellationToken ct = default);
        public Task<ICollection<StorePayments>> GetAllAsync();
        public Task<StorePayments> GetByIdAsync(long id);
        public Task DeleteAsync(long id);
    }
}
