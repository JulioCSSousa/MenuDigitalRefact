using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities;
using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;


namespace MenuDigital.Infrastructure.Repositories.MenuRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(OrderList order,CancellationToken ct)
        {
            await _context.OrderList.AddAsync(order);
        }

        public async Task Delete(OrderList model)
        {
            await _context.OrderList.AddAsync(model);
        }

        public async Task<IEnumerable<OrderList>> GetAll(CancellationToken ct)
        {
            return await _context.OrderList.ToListAsync();
        }

        public async Task<OrderList> GetById(long id, CancellationToken ct)
        {
            return await _context.OrderList.FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task Update(long id, OrderList order, CancellationToken ct)
        {
            _context.Update(order);
        }
    }
}
