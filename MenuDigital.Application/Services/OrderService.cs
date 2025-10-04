using MenuDigital.Application.Interfaces;
using MenuDigital.Application.Interfaces.Menu;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.ValuesObjects.Enum;


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

        public async Task<IEnumerable<OrderList>> GetAll(
    string? status, string? paymentForm, string? deliveryForm, CancellationToken ct)
        {
            var result = await _repository.GetAll(ct);

            var query = result.AsEnumerable();

            if (Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                query = query.Where(o => o.Status == parsedStatus);

            if (Enum.TryParse<PaymentForm>(paymentForm, true, out var parsedPayment))
                query = query.Where(o => o.PaymentForm == parsedPayment);

            if (Enum.TryParse<DeliveryForm>(deliveryForm, true, out var parsedDelivery))
                query = query.Where(o => o.DeliveryForm == parsedDelivery);

            return query.ToList();
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
