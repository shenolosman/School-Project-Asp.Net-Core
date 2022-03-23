namespace EventiaWebapp.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerMail { get; set; }
        public string OrganizerPhone { get; set; }
        public List<Event> Events { get; set; }
    }
}
