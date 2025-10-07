using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Store
{
    public class CategoryGetAll
    {
        public long CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
