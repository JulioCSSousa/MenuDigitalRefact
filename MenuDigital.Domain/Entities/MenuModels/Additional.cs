using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities.MenuModels
{
    public class Additional
    {
        [Key]
        public Guid Id { get; set; }
        public string? Category { get; set; }

        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Size { get; set; }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 0;
        public string[]? ProductIdList { get; set; }

    }
}
