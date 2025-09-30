using MenuDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.DTOs.Products.Request.Update
{
    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }

        public List<ProductModel>? Products { get; set; } = new List<ProductModel>();
        public List<StoreModel>? Stores { get; set; } = new List<StoreModel>();
    }
}
