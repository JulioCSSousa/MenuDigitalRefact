using MenuDigital.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;


namespace MenuDigital.Application.DTOs.Products.Request.Update
{
    public class CombinedProductUpdateDto
    {
        public string? Type { get; set; }
        public string? Category { get; set; }
        public bool? MainMenu { get; set; }

        public string? Name { get; set; }
        public string? Size { get; set; }

        public int? Min { get; set; } = 0;

        public int? Max { get; set; } = 0;

        [NotMapped]
        public List<Price>? Prices { get; set; } = new();
    }
}
