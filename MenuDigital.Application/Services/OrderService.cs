using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(OrderList order, CancellationToken ct)
        {
            await _repository.Create(order, ct);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(long id, CancellationToken ct)
        {
            var dbResult = await _repository.GetById(id, ct);
            if (dbResult == null)
            {
                return;
            }
            await _repository.Delete(dbResult);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderList>> GetAll(string? status, string? paymentForm, string? deliveryForm, CancellationToken ct)
        {
            var result = await _repository.GetAll(ct);

            var filteredResult = new List<OrderList>();
            if (!String.IsNullOrEmpty(status) && !String.IsNullOrEmpty(paymentForm) && !String.IsNullOrEmpty(deliveryForm))
                filteredResult = result.Select(o => o).Where(s => s.Status.ToString() == status)
                    .Where(p => p.PaymentForm.ToString() == paymentForm)
                    .Where(d => d.DeliveryForm.ToString() == deliveryForm).ToList();

            else if (!String.IsNullOrEmpty(status) && !String.IsNullOrEmpty(paymentForm))
            {
                filteredResult = result.Select(o => o).Where(s => s.Status.ToString() == status)
                    .Where(p => p.PaymentForm.ToString() == paymentForm).ToList();
            }
            else if (!String.IsNullOrEmpty(status))
            {
                filteredResult = result.Select(o => o).Where(s => s.Status.ToString() == status).ToList();
            }
            else
            {
                filteredResult = result.ToList();
            }
            return filteredResult;
        }

        public async Task<OrderList> GetById(long id, CancellationToken ct)
        {
            var result = await _repository.GetById(id, ct);
            if (result == null)
            {
                return new OrderList();
            }
            return result;
        }

        public async Task Update(long id, OrderList order, CancellationToken ct)
        {
            await _repository.Update(id, order, ct);
        }
    }
}
