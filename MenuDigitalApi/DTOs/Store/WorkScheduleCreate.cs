namespace MenuDigitalApi.DTOs.Store
{
    public class WorkScheduleCreate
    {
        public DateTime Day { get; private set; }
        public bool IsSelected { get; private set; }
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }
    }
}
