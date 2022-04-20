using EventiaWebapp.Data;
using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity;

namespace EventiaWebapp.Service;

public class Database
{
    private readonly EventDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public Database(EventDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    private async Task Seed()
    {
        await _roleManager.CreateAsync(new IdentityRole(MyRole.Attendee));
        await _roleManager.CreateAsync(new IdentityRole(MyRole.Organizer));
        await _roleManager.CreateAsync(new IdentityRole(MyRole.Admin));

        var hostedEventList = new List<Event>
        {
            new()
            {
                Description = "Culture",
                Title = "Vegan for beginners",
                Location = "Malmo",
                Date = new DateTime(2022, 10, 25),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            },
            new()
            {
                Description = "Rock",
                Title = "Sweden Spring Music",
                Location = "Stockholm",
                Date = new DateTime(2022, 11, 20),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            },
            new()
            {
                Description = "Pop",
                Title = "Miss Li", Location = "Stockholm",
                Date = new DateTime(2022, 12, 1),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            },
            new()
            {
                Description = "Rap/RnB",
                Title = "Bruno Mars",
                Location = "Stockholm",
                Date = new DateTime(2022, 04, 11),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            },
            new()
            {
                Description = "Rock",
                Title = "Korn",
                Location = "Malmo",
                Date = new DateTime(2022, 05, 12),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            },
            new()
            {
                Description = "Rock",
                Title = "Metallica",
                Location = "Goteborg",
                Date = new DateTime(2022, 06, 15),
                SeatsAvailable = 100,
                ImageName = "default-img.jpg"
            }
        };

        var attendeeUser = new User() { UserName = "Attendee", Email = "attendee@mail.com", JoinedEvents = new List<Event>() };

        var organizerUser = new User() { UserName = "Organizer", Email = "Organizer@mail.com", HostedEvents = hostedEventList, isOrganizer = true };

        var adminUser = new User() { UserName = "Admin", Email = "admin@mail.com" };

        await _userManager.CreateAsync(attendeeUser, "Passw0rd!");
        await _userManager.CreateAsync(organizerUser, "Passw0rd!");
        await _userManager.CreateAsync(adminUser, "Passw0rd!");

        await _userManager.AddToRoleAsync(attendeeUser, MyRole.Attendee);
        await _userManager.AddToRoleAsync(organizerUser, MyRole.Organizer);
        await _userManager.AddToRoleAsync(adminUser, MyRole.Admin);

        await _dbContext.SaveChangesAsync();
    }

    private async Task Recreate()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task RecreateAndSeed()
    {
        await Recreate();
        await Seed();
    }

    public async Task CreateIfNotExist()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task CreateAndSeedIfNotExist()
    {
        bool didCreateDatabase = await _dbContext.Database.EnsureCreatedAsync();
        if (didCreateDatabase)
        {
            await Seed();
        }
    }
}