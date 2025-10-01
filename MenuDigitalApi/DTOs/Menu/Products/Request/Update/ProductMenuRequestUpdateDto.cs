using MenuDigital.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Update
{
    public class ProductMenuRequestUpdateDto
    {

        [MaxLength(100)]
        public string? Name { get; set; } = default!;

        [MaxLength(100)]
        public List<Category>? Category { get; set; } = new List<Category>();

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool? IsSale { get; set; }

        [MaxLength(300)]
        public string? ImgUrl { get; set; }

        public int? ExtraIndex { get; set; }

        [MaxLength(300)]
        public List<string>? Observations { get; set; } = new();

        public List<Price>? Prices { get; set; } = new();

        public List<PreviewPrice>? PreviewPrices { get; set; } = new();
        public bool? CombinedPrice { get; set; }

        public bool? Multiple { get; set; }

        public List<CombinedProductUpdateDto>? CombinedProducts { get; set; } = new();

    }


}
