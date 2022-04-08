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
        await _roleManager.CreateAsync(new IdentityRole("Attendee"));
        await _roleManager.CreateAsync(new IdentityRole("Organisator"));
        await _roleManager.CreateAsync(new IdentityRole("Admin"));


        var attendesEvents1 = new List<Event>
        {
            new()
            {
                Descriptiton = "Rap/RnB",
                Title = "Bruno Mars",
                Location = "Stockholm",
                Date = new DateTime(2022, 04, 11),
                SeatsAvailable = 100
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Korn",
                Location = "Malmo",
                Date = new DateTime(2022, 05, 12),
                SeatsAvailable = 100
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Metallica",
                Location = "Goteborg",
                Date = new DateTime(2022, 06, 15),
                SeatsAvailable = 100
            }
        };
        var hostedEventList = new List<Event>
        {
            new()
            {
                Descriptiton = "Culture",
                Title = "Vegan for beginners",
                Location = "Malmo",
                Date = new DateTime(2022, 10, 25),
                SeatsAvailable = 100
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Sweden Spring Music",
                Location = "Stockholm",
                Date = new DateTime(2022, 11, 20),
                SeatsAvailable = 100

            },
            new()
            {
                Descriptiton = "Pop",
                Title = "Miss Li", Location = "Stockholm",
                Date = new DateTime(2022, 12, 1),
                SeatsAvailable = 100
            }
        };

        var organisatorUser = new User() { UserName = "organisator@mail.com", Email = "organisator@mail.com", HostedEvents = hostedEventList };
        var attendeeUser = new User() { UserName = "attendee@mail.com", Email = "attendee@mail.com", JoinedEvents = attendesEvents1 };
        var adminUser = new User() { UserName = "admin@mail.com", Email = "admin@mail.com" };


        await _userManager.CreateAsync(attendeeUser, "Passw0rd!");
        await _userManager.CreateAsync(organisatorUser, "Passw0rd!");
        await _userManager.CreateAsync(adminUser, "Passw0rd!");

        foreach (var item in attendesEvents1)
        {
            var orga = item.Organizer = organisatorUser;
            attendeeUser.HostedEvents = orga.HostedEvents;
        }
        //attendeeUser.JoinedEvents = attendesEvents1[0];
        //organisatorUser.HostedEvents = hostedEventList[0];

        await _userManager.AddToRoleAsync(attendeeUser, "Attendee");
        await _userManager.AddToRoleAsync(organisatorUser, "Organisator");
        await _userManager.AddToRoleAsync(adminUser, "Admin");

        #region old seed lists
        /*
        var organizersList = new List<Organizer>
        {
            new()
            {
                Name = "Xenol Organisation",
                Mail = "xenol@organisation.com",
                Phone = "04443332211"
            },
            new()
            {
                Name = "Bjorn Organisation",
                Mail = "bjorn@organisation.com",
                Phone = "04443332200"
            }
        };

        var eventsList = new List<Event>
        {
            new()
            {
                Descriptiton = "Rap/RnB",
                Title = "Bruno Mars",
                Location = "Stockholm",
                Date = new DateTime(2022, 04, 11),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Korn",
                Location = "Malmo",
                Date = new DateTime(2022, 05, 12),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Metallica",
                Location = "Goteborg",
                Date = new DateTime(2022, 06, 15),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Rap/RnB",
                Title = "Sagopa Kajmer",
                Location = "Istanbul",
                Date = new DateTime(2022, 07, 19),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Rap/RnB",
                Title = "Ezhel",
                Location = "Berlin",
                Date = new DateTime(2022, 08, 1),
                SeatsAvailable = 100,
                Organizer = organizersList[1]
            },
            new()
            {
                Descriptiton = "Pop",
                Title = "Britney Spears",
                Location = "New York",
                Date = new DateTime(2022, 09, 21),
                SeatsAvailable = 100,
                Organizer = organizersList[1]
            },
            new()
            {
                Descriptiton = "Culture",
                Title = "Vegan for beginners",
                Location = "Malmo",
                Date = new DateTime(2022, 10, 25),
                SeatsAvailable = 100,
                Organizer = organizersList[1]
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Sweden Spring Music",
                Location = "Stockholm",
                Date = new DateTime(2022, 11, 20),
                SeatsAvailable = 100,
                Organizer = organizersList[1]
            },
            new()
            {
                Descriptiton = "Pop",
                Title = "Miss Li", Location = "Stockholm",
                Date = new DateTime(2022, 12, 1),
                SeatsAvailable = 100,OrganizerId = 1,
                Organizer = organizersList[1]
            }
        };


               var attendeesList = new List<Attendee>
        {
            new()
            {
                Name = "Shenol",
                Email = "shenol@shenol.com",
                PhoneNummer = "0721112233"
            },
            new()
            {
                Name = "Dennis",
                Email = "dennis@shenol.com",
                PhoneNummer = "0721112244"
            }
        };

        await _dbContext.AddRangeAsync(organizersList);
        await _dbContext.AddRangeAsync(eventsList);
        await _dbContext.AddRangeAsync(attendeesList);
        */
        #endregion



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