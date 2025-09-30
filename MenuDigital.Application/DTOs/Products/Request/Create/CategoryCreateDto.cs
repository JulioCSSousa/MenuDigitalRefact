using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuDigital.Application.DTOs.Products.Request.Create
{
    public class CategoryCreateDto
    {
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
