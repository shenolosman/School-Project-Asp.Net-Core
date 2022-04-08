using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.CodeAnalysis;
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
        return _dbContext.Events.ToList();
    }

    public User GetAttendee(string id)
    {
        return _dbContext.Users.Include(x => x.JoinedEvents).ThenInclude(c => c.Organizer).First(p => p.Id == id);
    }
    public void BookEvent(string attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Users
            .Include(x => x.JoinedEvents)
            .FirstOrDefault(x => x.Id == attendeeId);

        findEvent.Attendees.Add(findAttendee);
        //Kan addera spots
        _dbContext.SaveChanges();
    }

    public List<Event> GetMyEvents(User user)
    {
        return user.JoinedEvents.ToList();
    }

    public List<Event> GetOrganizerEvents(User organizer)
    {
        var events = _dbContext.Events.Include(x => x.Organizer)
            .Where(z => z.Organizer.Id == organizer.Id).ToList();
        return events;
    }
    public void AddEvent(Event newEvent)
    {
        _dbContext.Add(newEvent);
        _dbContext.SaveChanges();
    }

    //public void AddNewEvent(string title, string description, DateTime eventDate, string location, int seatsavailable, bool isOrganizer, User user)
    //{
    //    var newEvent = new Event()
    //    {
    //        Title = title,
    //        Date = eventDate,
    //        Descriptiton = description,
    //        Location = location,
    //        SeatsAvailable = seatsavailable
    //    };
    //    _dbContext.Add(newEvent);
    //    if (isOrganizer)
    //    {
    //        newEvent.Organizer = user;
    //    }

    //    _dbContext.SaveChanges();
    //}
}