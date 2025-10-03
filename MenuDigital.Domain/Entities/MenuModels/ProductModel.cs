using MenuDigital.Domain.Entities.MenuModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities
{
    public class ProductModel
    {
        [Key]
        public Guid ProductId { get; set; } = default!;

        [ForeignKey("MenuId")]
        [Required(ErrorMessage = "You cannot create a Product without a Menu")]
        public Guid MenuId { get; set; } 

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public List<Category>? Category { get; set; } =  new List<Category>();

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

        public List<CombinedProduct>? CombinedProducts { get; set; } = new();
    }

    [NotMapped]
    public class Price
    {
        [MaxLength(100)]
        public string Label { get; set; } = default!;

        public decimal Value { get; set; } = 0;
    }
    [NotMapped]
    public class PreviewPrice
    {
        [MaxLength(100)]
        public string Label { get; set; } = default!;

        public decimal Value { get; set; }
    }



}
