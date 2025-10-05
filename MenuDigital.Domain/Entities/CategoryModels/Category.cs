using MenuDigital.Domain.Entities.ValuesObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities
{
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }

}
