using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Application.DTOs.Menu
{
    public class MenuCreate
    {
        public int Index { get; set; }
        [ForeignKey("StoreId")]
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public string? MenuName { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
