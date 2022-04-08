using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Data
{
    public class EventDbContext : IdentityDbContext<User>
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }
        //organizer and attendess will be deleted
        //  public DbSet<Organizer> Organizer { get; set; }
        //   public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
