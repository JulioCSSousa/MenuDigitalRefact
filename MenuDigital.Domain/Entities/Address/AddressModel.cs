using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Domain.Entities;

public class AddressModel
{
    [Key]
    public Guid AddressId { get; set; }
    [MaxLength(100)]
    public string? ZipCode { get; set; }
    [MaxLength(200)]
    public string? Street { get; set; }
    [MaxLength(20)]
    public string? Number { get; set; }
    [MaxLength(100)]
    public string? neighborhood { get; set; }
    [MaxLength(100)]
    public string? City { get; set; }
    [MaxLength(100)]
    public string? Complement { get; set; }


}
