using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.Interfaces.Menu
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderList>> GetAll(CancellationToken ct);
        public Task<OrderList> GetById(long id, CancellationToken ct);
        public Task Create(OrderList model, CancellationToken ct);
        public Task Update(long id, OrderList model, CancellationToken ct);
        public Task Delete(OrderList model);
    }
}
