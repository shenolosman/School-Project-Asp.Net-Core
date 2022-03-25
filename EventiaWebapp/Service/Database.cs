using EventiaWebapp.Models;

namespace EventiaWebapp.Service;

public class Database
{
    private EventDbContext _dbContext;

    public Database(EventDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
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
                Id = 2, Name = "Bjorn Organisation",
                Mail = "bjorn@organisation.com",
                Phone = "04443332200"
            }
        };
        _dbContext.AddRangeAsync(organizersList);
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
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Pop",
                Title = "Britney Spears",
                Location = "New York",
                Date = new DateTime(2022, 09, 21),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Culture",
                Title = "Vegan for beginners",
                Location = "Malmo",
                Date = new DateTime(2022, 10, 25),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Rock",
                Title = "Sweden Spring Music",
                Location = "Stockholm",
                Date = new DateTime(2022, 11, 20),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            },
            new()
            {
                Descriptiton = "Pop",
                Title = "Miss Li", Location = "Stockholm",
                Date = new DateTime(2022, 12, 1),
                SeatsAvailable = 100,
                Organizer = organizersList[0]
            }
        };
        _dbContext.AddRangeAsync(eventsList);
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
        _dbContext.AddRangeAsync(attendeesList);
    }

    public void PrepDatabase()
    {
        using var ctx = new EventDbContext();

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
        Seed();

    }
}