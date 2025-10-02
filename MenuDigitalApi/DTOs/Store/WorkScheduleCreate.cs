namespace MenuDigitalApi.DTOs.Store
{
    public class WorkScheduleCreate
    {
        public DateTime Day { get; set; }
        public bool IsSelected { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
