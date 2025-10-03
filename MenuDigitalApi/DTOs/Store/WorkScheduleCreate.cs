using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MenuDigitalApi.DTOs.Store
{
    public class WorkScheduleCreate
    {
        [Required(ErrorMessage = "Day is required.")]
        [DataType(DataType.DateTime, ErrorMessage = " Invalid Format Day.")]
        public DateTime Day { get; set; }
        [Required(ErrorMessage = "IsSelected is required.")]
        public bool IsSelected { get; set; }
        [Required(ErrorMessage = "Start time is required. Ex: 08:00:00")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$", ErrorMessage = "Start must be in HH:mm or HH:mm:ss format.")]
        public string Start { get; set; }
        [Required(ErrorMessage = "Start time is required.")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$", ErrorMessage = "Start must be in HH:mm or HH:mm:ss format.")]
        public string End { get; set; }
    }
}
