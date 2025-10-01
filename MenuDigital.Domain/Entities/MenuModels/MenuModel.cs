using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities.MenuModels
{
    public class MenuModel
    {
        [Key]
        public Guid MenuId { get; set; }
        public int Index { get; set; }
        [ForeignKey("StoreId")]
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string? MenuName { get; set; }
        [Required]
        public bool Active { get; set; }
        public List<ProductModel>? Products { get; set; }
    }
}
