namespace EventiaWebapp.Models
{
    public class Event
    {
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }

        public DateTime ThisYearsEvent(DateTime today)
        {
            return new DateTime(DateTime.Today.Year, EventDate.Month, EventDate.Day);
        }

    }
}
