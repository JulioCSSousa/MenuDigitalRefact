using MenuDigital.Domain.Entities;
using MenuDigitalApi.DTOs.Menu.Products.Response.CategoryResponse;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu
{
    public class ProductGetAllReponseDto
    {
        [Key]
        public Guid ProductId { get; set; } = default!;

        [ForeignKey("StoreId")]
        [Required(ErrorMessage = "You cannot create a Menu without a Store")]
        public string StoreId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public List<ProductCategoryResponse>? Category { get; set; } = new List<ProductCategoryResponse>();

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsSale { get; set; }

        [MaxLength(300)]
        public string? ImgUrl { get; set; }

        public int? ExtraIndex { get; set; }

        [MaxLength(300)]
        public List<string> Observations { get; set; } = new();

        public List<Price> Prices { get; set; } = new();

        public List<PreviewPrice> PreviewPrices { get; set; } = new();
        public bool CombinedPrice { get; set; }

        public bool Multiple { get; set; }

        public List<CombinedProductGetAllResponseDto>? CombinedProducts { get; set; } = new();
    }
}
