using System.ComponentModel.DataAnnotations;

namespace MenuDigitalApi.DTOs.Store
{
    public class WorkScheduleGetDto
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public bool IsSelected { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
