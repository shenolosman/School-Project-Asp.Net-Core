using EventiaWebapp.Data;
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
        return _dbContext.Events.ToList();
    }
    public User GetAttendee(string id) => _dbContext.Users.Include(x => x.JoinedEvents).ThenInclude(c => c.Organizer).First(p => p.Id == id);
    public void BookEvent(string attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Users
            .Include(x => x.JoinedEvents)
            .FirstOrDefault(x => x.Id == attendeeId);

        findEvent.Attendees.Add(findAttendee);
        //findEvent.SeatsAvailable--;
        _dbContext.SaveChanges();
    }
    public List<Event> GetMyEvents(User user) => user.JoinedEvents.ToList();
    public List<Event> GetOrganizerEvents(User organizer)
    {
        var events = _dbContext.Events.Include(x => x.Organizer)
            .Where(z => z.Organizer.Id == organizer.Id).ToList();
        return events;
    }
    public void AddEvent(Event newEvent, User user)
    {
        newEvent.Organizer = user;
        _dbContext.Add(newEvent);
        _dbContext.SaveChanges();
    }
    public async Task UpdateEvent(Event eventet)
    {
        _dbContext.Update(eventet);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteEvent(Event eventet)
    {
        _dbContext.Remove(eventet);
        await _dbContext.SaveChangesAsync();
    }
    public void DeleteMyEvent(string attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Users
            .Include(x => x.JoinedEvents)
            .FirstOrDefault(x => x.Id == attendeeId);

        findEvent.Attendees.Remove(findAttendee);
        //findEvent.SeatsAvailable++;
        _dbContext.SaveChanges();
    }
    public async Task ChangeStatusofUser(bool isorganizer, User user)
    {
        user.isOrganizer = isorganizer;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}