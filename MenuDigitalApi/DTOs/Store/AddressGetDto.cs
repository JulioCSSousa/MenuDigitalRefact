using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Store
{
    public class AddressGetDto
    {
        public long AddressId { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Complement { get; set; }
    }
}
