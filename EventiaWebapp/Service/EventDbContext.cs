using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventiaWebapp.Service
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .LogTo(m => Debug.WriteLine(m), LogLevel.Information)
                    .UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=EventiaDB;"
                    );
            }
        }
        public EventDbContext()
        {

        }
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
