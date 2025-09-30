
using MenuDigital.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Domain.Entities
{
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }

        public List<ProductModel>? Products { get; set; } = new List<ProductModel>();
        public List<StoreModel>? Stores { get; set; } = new List<StoreModel>();
    }
}
