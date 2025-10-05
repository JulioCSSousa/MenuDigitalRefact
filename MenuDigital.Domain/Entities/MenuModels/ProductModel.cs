using MenuDigital.Domain.Entities.MenuModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities
{
    public class ProductModel
    {
        [Key]
        public Guid ProductId { get; set; } = default!;

        [Required(ErrorMessage = "You cannot create a Product without a Store")]
        public Guid? StoreId { get; set; } 

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateOnly? InactivedDate { get; set; }

        [MaxLength(300)]
        public string? ImgUrl { get; set; }

        [MaxLength(300)]
        public string Observations { get; set; }

        public decimal Price { get; set; } = 0;

        public decimal PreviewPrice { get; set; }

        public List<Additional>? Additional { get; set; } = new();
    }

}
