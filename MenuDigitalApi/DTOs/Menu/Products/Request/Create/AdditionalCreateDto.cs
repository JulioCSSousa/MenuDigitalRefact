using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigitalApi.DTOs.Menu.Products.Request.Create
{
    public class AddtionalCreateDto
    {
        public string? Category { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Size { get; set; }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 0;
        public Guid[]? ProductIdList { get; set; }

    }
}
