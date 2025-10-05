using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigitalApi.DTOs.Menu.Products.Response.OrderResponse
{
    public class OrderedCombinedProductsDto
    {
        public string? Type { get; set; }
        public string Name { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; } = new();
    }
}
