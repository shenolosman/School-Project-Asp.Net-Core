using EventiaWebapp.Data;
using EventiaWebapp.Models;

namespace EventiaWebapp.Service;

public class Database
{
    private readonly EventDbContext _dbContext;

    public Database(EventDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task Seed()
    {
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