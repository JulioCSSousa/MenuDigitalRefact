using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.ValuesObjects;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Store
{
    public class StoreGetAllDto
    {
        public Guid StoreId { get; set; }
        public string? StoreName { get; set; }
        public List<CategoryGetAll>? Category { get; set; } = new List<CategoryGetAll>();
        public string? Description { get; set; }
        public string StoreUrl { get; set; }
        public bool? HasImage { get; set; }
        public bool? Closed { get; set; }
        public Colors? Colors { get; set; }
        public Images? Images { get; set; }
        public List<SocialMedia>? SocialMedias { get; set; }
        public Contact? Contacts { get; set; }
        public List<AddressGetDto>? Address { get; set; }
        public List<WorkScheduleGetDto>? WorkSchedule { get; set; }
        public List<StorePayments>? StorePayments { get; set; }
        public string? Alert { get; set; }
        public double? MinOrderPrice { get; set; }
    }
}
