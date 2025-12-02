using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;

namespace MenuDigital.Domain.Entities.MenuModels
{
    public class Additional
    {
        [Key]
        public Guid Id { get; set; }
        public string? Category { get; set; }

        [ForeignKey("ProductId")]
        [Required]
        public Guid? ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Size { get; set; }

        public int Min { get; set; } = 0;

        public int Max { get; set; } = 0;
        public ProductModel Product { get; set; } = new ProductModel();
        public string[]? ProductIdList { get; set; }

        public static string[] ToStringArray(Guid[] guidArray)
        {
            if(guidArray != null && guidArray.Length > 0)
            {
                var transform = guidArray.Select(x => x.ToString()).ToArray();
                return transform;
            }
            else
            {
                return [];
            }
            
        }

    }
}
