using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Update
{
    public class AdditionalUpdateDto
    {
        [Required]
        public Guid? Id { get; set; }
        public string? Category { get; set; }

        public string? Name { get; set; }
        public string? Size { get; set; }

        public int? Min { get; set; } = 0;

        public int? Max { get; set; } = 0;

        public Guid[]? ProductIdList { get; set; }
    }
}
