namespace EventiaWebapp.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }
        public string PhoneNummer { get; set; }
        public List<Event> ListEvent { get; set; }
    }
}
