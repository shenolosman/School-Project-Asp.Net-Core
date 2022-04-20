using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class User : IdentityUser
    {
        [InverseProperty("Organizer")]
        public IEnumerable<Event>? HostedEvents { get; set; }
        [InverseProperty("Attendees")]
        public IEnumerable<Event>? JoinedEvents { get; set; }
        public bool isOrganizer { get; set; } = false;
    }
}
