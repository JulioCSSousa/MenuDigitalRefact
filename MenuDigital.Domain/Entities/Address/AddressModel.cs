using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigital.Domain.Entities;

public class AddressModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AddressId { get; set; }
    [MaxLength(100)]
    public string? ZipCode { get; set; }
    [MaxLength(200)]
    public string? Street { get; set; }
    [MaxLength(20)]
    public string? Number { get; set; }
    [MaxLength(100)]
    public string? Neighborhood { get; set; }
    [MaxLength(100)]
    public string? City { get; set; }
    [MaxLength(100)]
    public string? State { get; set; }
    [MaxLength(100)]
    public string? Complement { get; set; }


    public StoreModel Store { get; set; }
    [ForeignKey("StoreId")]
    public Guid StoreId { get; set; }


}
