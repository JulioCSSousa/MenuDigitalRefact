using MenuDigital.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Update
{
    public class ProductUpdateDto
    {

        [MaxLength(100)]
        public string? Name { get; set; } = default!;

        [MaxLength(100)]
        public string? Category { get; set; } 
        [MaxLength(500)]
        public string? Description { get; set; }
        public bool? IsActived { get; set; } = true;
        public DateOnly? InactivedDate { get; set; }

        [MaxLength(300)]
        public string? ImgUrl { get; set; }


        [MaxLength(300)]
        public string? Observations { get; set; } 

        public decimal? Price { get; set; }

        public decimal? PreviewPrice { get; set; } = new();
        public List<AdditionalUpdateDto>? Additional { get; set; }

    }


}
