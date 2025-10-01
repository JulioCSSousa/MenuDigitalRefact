namespace MenuDigital.Domain.Entities
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public bool IsSelected { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }


    }
}
