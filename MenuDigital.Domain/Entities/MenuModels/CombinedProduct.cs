using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Domain.Entities.MenuModels
{
    public class CombinedProduct
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        [Required]
        public bool MainMenu { get; set; }

        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Size { get; set; }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 0;

        [NotMapped]
        public List<Price> Prices { get; set; } = new();
    }
}
