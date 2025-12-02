
using MenuDigital.Domain.Entities.ValuesObjects;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace MenuDigital.Domain.Entities   
{
    public class StoreModel
    {
        [Key]
        public Guid StoreId { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Store Name is Required")]
        public string? StoreName { get; set; }
        public List<Category>? Category { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Url is Required")]
        public string StoreUrl { get; set; } 
        public bool HasImage { get; set; }
        public bool Closed { get; set; }
        public Colors? Colors { get; set; }
        public Images? Images { get; set; }
        public List<SocialMedia>? SocialMedias { get; set; } = new();
        public Contact? Contacts { get; set; }
        public List<AddressModel>? Address { get; set; } = new();
        public List<WorkSchedule>? WorkSchedule { get; set; }
        public List<StorePayments>? StorePayments { get; set; }
        public string? Alert { get; set; }
        public double? MinOrderPrice { get; set; }
        public List<ProductModel> Products { get; set; } = new();

    }

    [Owned]
    public class SocialMedia
    {
        public string? Name { get; set; }
        public string? Url { get; set; }

    }

    [Owned]
    public class Contact
    {
        public string[]? Phones { get; set; }
        public string[]? Whatsapps { get; set; }
        public string[]? Emails { get; set; }

    }
    [Owned]
    public class Images
    {
        [MaxLength(500)]
        public string? Logo { get; set; }
        [MaxLength(500)]
        public string? Header { get; set; }
    }

}
