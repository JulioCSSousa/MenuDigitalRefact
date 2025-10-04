
using MenuDigital.Domain.Entities.ValuesObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Url is Required")]
        public string StoreUrl { get; set; }
        public bool HasImage { get; set; }
        public bool Closed { get; set; }
        [NotMapped]
        public Colors? Colors { get; set; }
        [NotMapped]
        public Images? Images { get; set; }
        [NotMapped]
        public SocialMedia? SocialMedias { get; set; }
        [NotMapped]
        public Contact? Contacts { get; set; }
        public List<AddressModel>? Address { get; set; } 
        public List<WorkSchedule>? WorkSchedule { get; set; }
        public List<StorePayments>? StorePayments { get; set; }
        public string? Alert { get; set; }
        public double? MinOrderPrice { get; set; }

    }

    [NotMapped]
    public class SocialMedia
    {
        public List<string> Facebook { get; set; } = new();
        public List<string> Instagram { get; set; } = new();
        public List<string> Whatsapp { get; set; } = new();
        public List<string> X { get; set; } = new();
        public List<string> Website { get; set; } = new();

    }

    [NotMapped]
    public class Contact
    {
        public List<string>? Phones { get; set; } = new List<string>();
        public List<string>? Whatsapps { get; set; } = new List<string>();
        public List<string>? Emails { get; set; } = new List<string>();

    }
    [NotMapped]
    public class Images
    {
        [MaxLength(500)]
        public string? Logo { get; set; }
        [MaxLength(500)]
        public string? Header { get; set; }
    }

}
