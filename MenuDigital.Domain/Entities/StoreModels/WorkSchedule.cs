namespace MenuDigital.Domain.Entities
{
    public class WorkSchedule
    {
        public int Id { get; private set; }
        public DateTime Day { get; private set; }
        public bool IsSelected { get; private set; }
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }

        private WorkSchedule() { }
        public WorkSchedule(DateTime day, bool isSelected, TimeSpan start, TimeSpan end)
        {
            if (end <= start)
                throw new ArgumentException("End time must be after start time.", nameof(end));

            if (day == default)
                throw new ArgumentException("Day cannot be empty.", nameof(day));

            Day = day;
            IsSelected = isSelected;
            Start = start;
            End = end;
        }


        public void SetDay(DateTime day)
        {
            Day = day;
        }
        public void SetIsSelected(bool isSelected)
        {
            IsSelected = isSelected;
        }
        public void SetStart(TimeSpan start)
        {
            Start = start;
        }
        public void SetEnd(TimeSpan end)
        {
            End = end;
        }
    }

}
