using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MenuDigital.Domain.Entities
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Formato inválido para Day.")]
        public DateTime Day { get; set; }

        public bool IsSelected { get; set; }
        [DataType(DataType.Time, ErrorMessage = "Formato inválido para Start. Use HH:mm:ss")]
        public TimeSpan Start { get; set; }
        [DataType(DataType.Time, ErrorMessage = "Formato inválido para End. Use HH:mm:ss")]
        public TimeSpan End { get; set; }


    }
}
