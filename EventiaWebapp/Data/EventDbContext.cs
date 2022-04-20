using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Data
{
    public class EventDbContext : IdentityDbContext<User> //IdentityDbContext<User, MyRole, string>
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
    }
}
