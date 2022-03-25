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

    //public List<Event> Events { get; set; }
    //public List<Event> MyEvents { get; set; }
    //public List<Attendee> Attendees { get; set; }
    //public List<Organizer> Organizers { get; set; }

    //public void GetEventsXDaysFromDate(DateTime date)
    //{
    //    Events.Sort((eventA, eventB) =>
    //        DateTime.Compare(eventA.ThisYearsEvent(date), eventB.ThisYearsEvent(date)));
    //}

    public List<Event> GetEvents()
    {
        return _dbContext.Events.Include(o => o.Organizer).Include(a => a.Attendees).ToList();
    }

    public Attendee GetAttendes(int id)
    {
        return _dbContext.Attendees.Include(x => x.ListEvent).First(p => p.Id == id);
    }

    //public Event FindId(int id)
    //{
    //    return Events.Find(x => x.Id == id);
    //}
    public void BookEvent(Attendee attendee, Event events)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == events.Id);

        var findAttendee = _dbContext.Attendees
            .Include(x => x.ListEvent)
            .FirstOrDefault(x => x.Id == attendee.Id);

        findEvent.Attendees.Add(findAttendee);

        _dbContext.SaveChanges();
    }

    public List<Event> GetMyEvents(int id)
    {
        var attende = _dbContext.Attendees.Include(x => x.ListEvent).ThenInclude(z => z.Organizer)
            .FirstOrDefault(c => c.Id == id);

        var myevent = attende.ListEvent;
        return myevent.ToList();
    }
}