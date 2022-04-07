using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class Event
    {
        [Key] public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Descriptiton { get; set; }
        public int SeatsAvailable { get; set; }

        public ICollection<Attendee>? Attendees { get; set; }
        [ForeignKey("Organizer")] public int OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }


        public List<User> Organizers { get; set; }
        public List<User> Attendeess { get; set; }

        //public DateTime ThisYearsEvent(DateTime today)
        //{
        //    return new DateTime(DateTime.Today.Year, Date.Month, Date.Day);
        //}
    }
}
