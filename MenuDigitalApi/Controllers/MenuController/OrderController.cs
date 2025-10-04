using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.ValuesObjects.Enum;
using MenuDigitalApi.DTOs;
using MenuDigitalApi.DTOs.Menu.Products.Response.OrderResponse;
using Microsoft.AspNetCore.Mvc;

namespace MenuDigitalApi.Controllers.MenuController
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService _service;
        public OrderController(OrderService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> Create(OrderListCreateDto dto, CancellationToken ct)
        {
            var dbModel = new OrderList
            {
                StoreId = dto.StoreId,
                DeliveryForm = dto.DeliveryForm,
                OrderedAt = TimeSpan.Parse(dto.FinishedAt),
                FinishedAt = TimeSpan.Parse(dto.FinishedAt),
                PaymentForm = dto.PaymentForm,
                Status = dto.Status,
                UserId = dto.UserId,
                ProductIds = dto.ProductIds
            };
            await _service.Create(dbModel, ct);
            return Ok(dbModel.OrderId);

        }
        [HttpDelete]
        public async Task Delete(long id, CancellationToken ct)
        {
            await _service.Delete(id, ct);
        }
        [HttpGet]
        public async Task<IEnumerable<OrderListGetAllDto>> GetAll(string? status, string? paymentForm, string? deliveryForm, CancellationToken ct)
        {

            var dbModel = await _service.GetAll(status, paymentForm, deliveryForm, ct);
            var dtoModel = new List<OrderListGetAllDto>();

            foreach (var item in dbModel)
            {
                var delivery = item.DeliveryForm.ToString();
                dtoModel.Add(new OrderListGetAllDto
                {
                    OrderId = item.OrderId,
                    DeliveryForm = item.DeliveryForm.ToString(),
                    FinishedAt = item.FinishedAt.ToString(),
                    OrderedAt = item.OrderedAt.ToString(),
                    PaymentForm = item.PaymentForm.ToString(),
                    Status = item.Status.ToString(),
                    ProductIds = item.ProductIds,
                    StoreId = item.StoreId,
                    UserId = item.UserId
                }
                );
            }
            return dtoModel;
        }

        [HttpGet("{id}")]
        public async Task<OrderList> GetById(long id, CancellationToken ct)
        {
            return await _service.GetById(id, ct);
        }
        [HttpPut]
        public async Task Update(long id, OrderList order, CancellationToken ct)
        {
            await _service.Update(id, order, ct);
        }
    }
}
