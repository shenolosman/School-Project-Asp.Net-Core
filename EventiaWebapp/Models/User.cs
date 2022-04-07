using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class User : IdentityUser
    {
        [InverseProperty("Organizers")]
        public Event HostedEvents { get; set; }
        [InverseProperty("Attendeess")]
        public int? JoinedEventsId { get; set; }
        public List<Event>? JoinedEvents { get; set; }
    }
}
