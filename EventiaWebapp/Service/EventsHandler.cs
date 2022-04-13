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
    public async Task<List<Event>> GetEvents() => await _dbContext.Events.ToListAsync();
    public async Task<User?> GetAttendee(string id) => await _dbContext.Users.Include(x => x.JoinedEvents)!.ThenInclude(c => c.Organizer).FirstOrDefaultAsync(p => p.Id == id);
    public async Task BookEvent(string attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Users
            .Include(x => x.JoinedEvents)
            .FirstOrDefault(x => x.Id == attendeeId);


        findEvent?.Attendees?.Add(findAttendee);
        //findEvent.SeatsAvailable--;
        await _dbContext.SaveChangesAsync();
    }
    public async Task<List<Event>> GetOrganizerEvents(User organizer) => await _dbContext.Events.Include(x => x.Organizer)
            .Where(z => z.Organizer.Id == organizer.Id).ToListAsync();
    public async Task AddEvent(Event newEvent, User user)
    {
        newEvent.Organizer = user;
        await _dbContext.AddAsync(newEvent);
        await _dbContext.SaveChangesAsync();
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
    public async Task DeleteMyEvent(string attendeeId, int eventId)
    {
        var findEvent = _dbContext.Events
            .Include(x => x.Attendees)
            .FirstOrDefault(x => x.Id == eventId);

        var findAttendee = _dbContext.Users
            .Include(x => x.JoinedEvents)
            .FirstOrDefault(x => x.Id == attendeeId);



        findEvent?.Attendees?.Remove(findAttendee);
        //findEvent.SeatsAvailable++;
        await _dbContext.SaveChangesAsync();
    }
    public async Task ChangeStatusofUser(bool isorganizer, User user)
    {
        user.isOrganizer = isorganizer;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}