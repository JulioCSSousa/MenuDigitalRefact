using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.ValuesObjects;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigitalApi.DTOs.Store
{
    public class StoreCreateDto
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Store name is required")]
        public string? StoreName { get; set; }
        public List<CategoryCreateDto>? Category { get; set; } = new List<CategoryCreateDto>();
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Url is Required")]
        public string StoreUrl { get; set; }
        public bool HasImage { get; set; }
        public bool Closed { get; set; }
        public Colors? Colors { get; set; }
        public Images? Images { get; set; }
        public List<SocialMedia>? SocialMedias { get; set; }
        public Contact? Contacts { get; set; }
        public List<AddressModel>? Address { get; set; } 
        public List<WorkScheduleCreate>? WorkSchedule { get; set; }
        public List<StorePaymentsCreate>? StorePayments { get; set; }
        public string? Alert { get; set; }
        public double? MinOrderPrice { get; set; }
    }
}
