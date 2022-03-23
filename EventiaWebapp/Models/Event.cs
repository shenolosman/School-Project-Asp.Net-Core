namespace EventiaWebapp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public string Descriptiton { get; set; }

        
        public List<Attendee> ListAttendee { get; set; }
        public Organizer Organizer { get; set; }


        public DateTime ThisYearsEvent(DateTime today)
        {
            return new DateTime(DateTime.Today.Year, EventDate.Month, EventDate.Day);
        }
    }
}
