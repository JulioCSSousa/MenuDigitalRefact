using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Create
{
    public class CategoryCreateDto
    {
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Icon { get; set; }
    }
}
