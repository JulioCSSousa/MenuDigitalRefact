using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.ValuesObjects;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Store
{
    public class StoreUpdateDto
    {
        [MaxLength(100)]
        public string? StoreName { get; set; }
        public List<CategoryCreateDto>? Category { get; set; } = new List<CategoryCreateDto>();
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Store StoreUrl is required")]
        public string StoreUrl { get; set; }
        public bool? HasImage { get; set; }
        public bool? Closed { get; set; }
        public Colors? Colors { get; set; }
        public Images? Images { get; set; }
        public SocialMedia? SocialMedias { get; set; }
        public Contact? Contacts { get; set; }
        public List<AddressModel>? Address { get; set; }
        public List<WorkScheduleCreate>? WorkSchedule { get; set; }
        public List<StorePayments>? StorePayments { get; set; }
        public string? Alert { get; set; }
        public double? MinOrderPrice { get; set; }
    }
}
