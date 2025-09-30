


using MenuDigital.Domain.Models.Entities;

namespace MenuDigital.Domain.Entities
{
    internal class Tenant : User
    {
        public Tenant() { }

        public string ActivePlan { get; set; }
        public double SaleScore { get; set; }
        public string StoreId { get; set; }
        public StorePayments PaymentForm { get; set; }
    }
}
