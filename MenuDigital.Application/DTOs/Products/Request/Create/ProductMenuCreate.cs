using MenuDigital.Application.DTOs.Products.Request.Create;
using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.DTOs.Products.Request.Update.Create
{
    public class ProductMenuCreate
    {

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public List<CategoryCreateDto>? Category { get; set; } = new List<CategoryCreateDto>();
        public string? StoreId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsSale { get; set; }

        [MaxLength(300)]
        public string? ImgUrl { get; set; }

        public int? ExtraIndex { get; set; }

        [MaxLength(300)]
        public List<string> Observations { get; set; } = new();

        public List<Price> Prices { get; set; } = new();

        public List<PreviewPrice> PreviewPrices { get; set; } = new();
        public bool CombinedPrice { get; set; }

        public bool Multiple { get; set; }

        public List<CombinedProductCreate>? CombinedProducts { get; set; } = new();

    }
}
