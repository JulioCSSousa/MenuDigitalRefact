using MenuDigital.Domain.Entities;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Create
{
    public class ProductMenuCreate
    {

        public string Name { get; set; } = default!;

        public List<CategoryCreateDto>? Category { get; set; } = new List<CategoryCreateDto>();
        public Guid MenuId { get; set; }

        public string? Description { get; set; }

        public bool IsSale { get; set; }

        public string? ImgUrl { get; set; }

        public int? ExtraIndex { get; set; }

        public List<string> Observations { get; set; } = new();

        public List<Price> Prices { get; set; } = new();

        public List<PreviewPrice> PreviewPrices { get; set; } = new();
        public bool CombinedPrice { get; set; }

        public bool Multiple { get; set; }

        public List<CombinedProductCreate>? CombinedProducts { get; set; } = new();

    }
}
