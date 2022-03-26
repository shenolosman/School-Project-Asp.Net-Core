using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Service;

public class EventsHandler
{
    private readonly EventDbContext _dbContext;

    public EventsHandler(EventDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Event> GetEvents()
    {
        return _dbContext.Events.Include(o => o.Organizer).Include(a => a.Attendees).ToList();
    }

    public Attendee GetAttendes(int id)
    {
        return _dbContext.Attendees.Include(x => x.ListEvent).First(p => p.Id == id);
    }
    public void BookEvent(int attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Attendees
            .Include(x => x.ListEvent)
            .FirstOrDefault(x => x.Id == attendeeId);

        findEvent?.Attendees?.Add(findAttendee);

        _dbContext.SaveChanges();
    }

    public List<Event> GetMyEvents(int id)
    {
        var attende = _dbContext.Attendees.Include(x => x.ListEvent).ThenInclude(z => z.Organizer)
            .FirstOrDefault(c => c.Id == id);

        return attende.ListEvent.ToList();
    }
}