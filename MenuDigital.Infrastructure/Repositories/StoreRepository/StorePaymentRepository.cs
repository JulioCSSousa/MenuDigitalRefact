using MenuDigital.Application.Interfaces.Store;
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;

namespace MenuDigital.Infrastructure.Repositories.StoreRepository
{
    public class StorePaymentRepository : IStorePaymentRepository
    {
        private readonly AppDbContext _context;
        public StorePaymentRepository(AppDbContext context) { 
        
            _context = context;
        }

        public async Task AddAsync(StorePayments addpayment, CancellationToken ct = default)
        {
                await _context.StorePayments.AddAsync(addpayment);
        }

        public async Task UpdateAsync(long paymentId, StorePayments addpayment, CancellationToken ct = default)
        {
            var payment = await _context.StorePayments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Update(addpayment);
            }
            return;
        }

        public async Task<ICollection<StorePayments>> GetAllAsync()
        {
            var dbPayments = await _context.StorePayments.ToListAsync();
            return dbPayments;
        }

        public async Task<StorePayments> GetByIdAsync(long id)
        {
            var dbPayments = await _context.StorePayments.FirstOrDefaultAsync(p => p.Id == id);
            return dbPayments;
        }

        public async Task DeleteAsync(long id)
        {
            await GetByIdAsync(id);
            _context.Remove(id);
        }
    }
}
