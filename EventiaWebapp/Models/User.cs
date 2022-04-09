using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class User : IdentityUser
    {
        //public User()
        //{
        //    Roles = new List<string>();
        //}
        [InverseProperty("Organizer")]
        public IEnumerable<Event>? HostedEvents { get; set; }
        [InverseProperty("Attendees")]
        public int? JoinedEventsId { get; set; }
        public IEnumerable<Event>? JoinedEvents { get; set; }
        public bool isOrganizer { get; set; } = false;

        // public List<string> Roles { get; set; }
    }
}
