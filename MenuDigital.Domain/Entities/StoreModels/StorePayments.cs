using MenuDigital.Domain.Entities.ValuesObjects.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MenuDigital.Domain.Entities
{
    public class StorePayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public PaymentForm? PaymentsCount { get; set; }
        [ForeignKey("StoreId")]
        public Guid StoreId { get; set; }
    }
}
