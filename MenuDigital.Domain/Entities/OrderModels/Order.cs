using MenuDigital.Domain.Entities.ValuesObjects.Enum;

using System.ComponentModel.DataAnnotations;


namespace MenuDigital.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? StoreId { get; set; }
        public string? UserId { get; set; }
        public string? DeliveryForm { get; set; }
        public PaymentForm PaymentForm { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string? AddressId { get; set; }
        public int? Rank { get; set; }
        public string? Feedback { get; set; }
        public string? OrderItemsId { get; set; }
    }
}
