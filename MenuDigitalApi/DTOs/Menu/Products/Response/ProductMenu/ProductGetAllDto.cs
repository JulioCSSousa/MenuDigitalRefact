using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Menu.Products.Response.CategoryResponse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu
{
    public class ProductGetAllDto
    {
        [Key]
        public Guid ProductId { get; set; } = default!;

        [ForeignKey("StoreId")]
        [Required(ErrorMessage = "You cannot create a Menu without a Store")]
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

        public decimal Price { get; set; }

        public decimal PreviewPrice { get; set; }


        public List<AdditionalGetAllDto>? Additional { get; set; } = new();
    }
}
