using System.ComponentModel.DataAnnotations;


namespace MenuDigital.Domain.Entities.ValuesObjects
{
    public class Colors
    {
        [MaxLength(100)]
        public string? Primary { get; set; }
        [MaxLength(100)]
        public string? Secondary { get; set; }
    }
}
