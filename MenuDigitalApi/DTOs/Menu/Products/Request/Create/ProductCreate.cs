using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Create
{
    public class ProductCreate
    {

        public string Name { get; set; } = default!;

        public string? Category { get; set; }
        public Guid StoreId { get; set; }

        public string? Description { get; set; }

        public DateOnly InactivedDate { get; set; }

        public string? ImgUrl { get; set; }

        public string Observations { get; set; }

        public decimal Price { get; set; }

        public decimal PreviewPrice { get; set; }

        public List<AddtionalCreateDto>? Additional { get; set; }

    }
}
