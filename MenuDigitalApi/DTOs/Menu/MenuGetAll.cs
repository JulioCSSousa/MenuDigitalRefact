using MenuDigital.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuDigitalApi.DTOs.Menu
{
    public class MenuGetDto
    {
        public Guid MenuId { get; set; }
        public int Index { get; set; }
        public Guid StoreId { get; set; }
        public string? MenuName { get; set; }
        public bool Active { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
