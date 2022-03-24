using EventiaWebapp.Models;

namespace EventiaWebapp.Service
{
    public class EventHandler
    {
        public List<Event> Events { get; set; }
        public List<Event> MyEvents { get; set; }
        public List<Attendee> Attendees { get; set; }
        public List<Organizer> Organizers { get; set; }
        public EventHandler()
        {
            Organizers = new List<Organizer>()
            {
                new(){Id=1,OrganizerName ="Xenol Organisation",OrganizerMail="xenol@organisation.com",OrganizerPhone="04443332211"},
                new(){Id=2,OrganizerName ="Bjorn Organisation",OrganizerMail="bjorn@organisation.com",OrganizerPhone="04443332200"}
            };
            Attendees = new List<Attendee>()
            {
                new(){Id = 1,AttendeeName ="Shenol",AttendeeEmail="shenol@shenol.com",ListEvent=new List<Event>(),PhoneNummer="0721112233"}
            };
            Events = new List<Event>()
            {
                new Event() {Id=1,Descriptiton="Rap/RnB" , EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 04, 11),Organizer=Organizers[1]},
                new Event() {Id=2,Descriptiton="Rock" ,EventName = "Korn", EventLocation = "Malmo", EventDate = new DateTime(2022, 05, 12),Organizer=Organizers[1]},
                new Event() {Id=3,Descriptiton="Rock" ,EventName = "Metallica", EventLocation = "Goteborg", EventDate = new DateTime(2022, 06, 15),Organizer=Organizers[1]},
                new Event() {Id=4,Descriptiton="Rap/RnB",EventName = "Sagopa Kajmer", EventLocation = "Istanbul", EventDate = new DateTime(2022, 07, 19),Organizer=Organizers[1]},
                new Event() {Id=5,Descriptiton="Rap/RnB" ,EventName = "Ezhel", EventLocation = "Berlin", EventDate = new DateTime(2022, 08, 1),Organizer=Organizers[0]},
                new Event() {Id=6,Descriptiton="Pop" ,EventName = "Britney Spears", EventLocation = "New York", EventDate = new DateTime(2022, 09, 21),Organizer=Organizers[0]},
                new Event() {Id=7,Descriptiton="Culture",EventName = "Vegan for beginners", EventLocation = "Malmo", EventDate = new DateTime(2022, 10, 25),Organizer=Organizers[0]},
                new Event() {Id=8,Descriptiton="Rock" ,EventName = "Sweden Spring Music", EventLocation = "Stockholm", EventDate = new DateTime(2022, 11, 20),Organizer=Organizers[0]},
                new Event() {Id=9,Descriptiton="Pop" ,EventName = "Miss Li", EventLocation = "Stockholm", EventDate = new DateTime(2022, 12, 1),Organizer=Organizers[0]},
            };
            MyEvents = new List<Event>();
        }
        public List<Event> GetEventsXDaysFromDate(DateTime date)
        {
            var getAllEvents = new List<Event>();
            Events.Sort((eventA, eventB) =>
                DateTime.Compare(eventA.ThisYearsEvent(date), eventB.ThisYearsEvent(date)));
            return getAllEvents;
        }
        public Event FindId(int id)
        {
            return Events.Find(x => x.Id == id);
        }
        public void JoinEvent(Event eventet, Attendee attende)
        {
            attende.ListEvent.Add(eventet);
            if (attende.ListEvent.Count >= 0)
            {

                MyEvents.AddRange(attende.ListEvent);

            }

        }
    }
}

