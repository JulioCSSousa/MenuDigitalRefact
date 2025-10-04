using MenuDigital.Domain.Entities.ValuesObjects.Enum;

namespace MenuDigitalApi.DTOs.Menu.Products.Response.OrderResponse
{
    public class OrderListGetAllDto
    {
        public long OrderId { get; set; }
        public string? StoreId { get; set; }
        public string? UserId { get; set; }
        public string DeliveryForm { get; set; }
        public string PaymentForm { get; set; }
        public string Status { get; set; }
        public string OrderedAt { get; set; }
        public string? FinishedAt { get; set; }
        public string[] ProductIds { get; set; }
    }
}
