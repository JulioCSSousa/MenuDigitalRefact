using MenuDigital.Domain.Entities.ValuesObjects.Enum;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;


namespace MenuDigital.Domain.Entities
{
    public class OrderList
    {
        [Key]
        public long OrderId { get; set; }
        public string? StoreId { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Delivery form is required")]
        public DeliveryForm DeliveryForm { get; set; }
        [Required(ErrorMessage = "Payment form is required")]
        public PaymentForm PaymentForm { get; set; }
        [Required(ErrorMessage = "Order status is required")]
        public OrderStatus Status { get; set; }
        public TimeSpan OrderedAt { get; set; }
        public TimeSpan? FinishedAt { get; set; }
        public Guid[] ProductIds { get; set; } = new Guid[0];
    }
}
