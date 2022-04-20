using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Service;

public class EventsHandler
{
    private readonly EventDbContext _dbContext;
    private readonly IWebHostEnvironment _hostEnvironment;
    public EventsHandler(EventDbContext dbContext, IWebHostEnvironment hostEnvironment)
    {
        _dbContext = dbContext;
        _hostEnvironment = hostEnvironment;
    }
    public async Task<List<Event>> GetEvents() => await _dbContext.Events.OrderBy(x => x.Date).ToListAsync();
    public async Task<Event?> GetEvent(int id) => await _dbContext.Events.FindAsync(id);
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
            .Where(z => z.Organizer.Id == organizer.Id).OrderBy(x => x.Date).ToListAsync();
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
    public async Task<string?> SaveImageFile(IFormFile? imageFile, string? currentImageName = "")
    {
        if (imageFile != null)
        {
            var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName).ToLower();
            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            var imageName = DateTime.Now.ToString("yymmssfff") + fileName + extension;

            //save new file
            var filePath = Path.Combine(_hostEnvironment.WebRootPath + "/img/" + imageName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            //delete old file when changing img
            if (!string.IsNullOrEmpty(currentImageName) && currentImageName != "default-img.jpg")
            {
                var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath + "/img/" + currentImageName);
                File.Delete(oldFilePath);
            }
            return imageName;
        }
        return currentImageName;
    }
    public async Task DeleteImageFile(Event? currentImageName)
    {
        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", currentImageName.ImageName);
        if (File.Exists(imagePath) && currentImageName.ImageName != "default-img.jpg")
            File.Delete(imagePath);
    }
}