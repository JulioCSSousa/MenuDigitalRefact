using MenuDigital.Application.DTOs.Products.Response.OrderResponse;
using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.DTOs.Products
{
    public class OrderedProducts
    {
        public Guid ProductId { get; set; } = default!;

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(300)]
        public List<string> Observations { get; set; } = new();

        public List<Price> Prices { get; set; } = new();

        public List<OrderedCombinedProductsDto>? CombinedProducts { get; set; } = new();
    }
}
