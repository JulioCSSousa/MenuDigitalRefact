using MenuDigital.Domain.Entities.ValuesObjects.Enum;

namespace MenuDigitalApi.DTOs
{
    public class OrderListCreateDto
    {
        public string? StoreId { get; set; }
        public string? UserId { get; set; }
        public DeliveryForm DeliveryForm { get; set; }
        public PaymentForm PaymentForm { get; set; }
        public OrderStatus Status { get; set; }
        public string OrderedAt { get; set; }
        public string? FinishedAt { get; set; }
        public Guid[] ProductIds { get; set; }
    }
}
