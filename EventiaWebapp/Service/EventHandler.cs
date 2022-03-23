using EventiaWebapp.Models;

namespace EventiaWebapp.Service
{
    public class EventHandler
    {
        public List<Event> Events { get; set; }
        public List<Event> MyEvents { get; set; }
        public List<Attendee> Attendees{ get; set; }
        public List<Organizer> Organizers{ get; set; }
        public EventHandler()
        {
            Organizers = new List<Organizer>()
            {
                new(){Id=1,OrganizerName ="Xenol Organisation",OrganizerMail="xenol@organisation.com",OrganizerPhone="04443332211",Events=new List<Event>()},
                new(){Id=2,OrganizerName ="Bjorn Organisation",OrganizerMail="bjorn@organisation.com",OrganizerPhone="04443332200",Events=new List<Event>()}
            };
            Attendees = new List<Attendee>()
            {
                new(){Id = 1,AttendeeName ="Shenol",AttendeeEmail="shenol@shenol.com",ListEvent=new List<Event>(),PhoneNummer="0721112233"}
            };           
            Events = new List<Event>()
            {
                new Event() {Id=1,Descriptiton="American singer-songwriter Bruno Mars has released three studio albums, one collaborative studio albums, one EP, 30 singles (seven as a featured artist) and three promotional singles. With estimated sales of over 26 million albums and 200 million singles worldwide, Mars is one of the best-selling artists of all time." , EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 04, 11),Organizer=Organizers[1]},
                new Event() {Id=2,Descriptiton="The discography of American nu metal band Korn consists of 14 studio albums, three live albums, eight compilation albums, seven video albums, three extended plays, 45 singles, 10 promotional singles and 50 music videos." ,EventName = "Korn", EventLocation = "Malmo", EventDate = new DateTime(2022, 05, 12),Organizer=Organizers[1]},
                new Event() {Id=3,Descriptiton="Metallica is an American heavy metal band. The band was formed in 1981 in Los Angeles by vocalist/guitarist James Hetfield and drummer Lars Ulrich, and has been based in San Francisco for most of its career. The band's fast tempos, instrumentals and aggressive musicianship made them one of the founding big four bands of thrash metal, alongside Megadeth, Anthrax and Slayer. " ,EventName = "Metallica", EventLocation = "Goteborg", EventDate = new DateTime(2022, 06, 15),Organizer=Organizers[1]},
                new Event() {Id=4,Descriptiton="Yunus Özyavuz (born 17 August 1978), better known by his stage name Sagopa Kajmer (Turkish: [sɑgopɑ kɑʒmæɾ]), is a Turkish rapper, songwriter, record producer, and DJ. He was born in 1978 in Samsun, and finished his primary and high school there. He then started working as a DJ in one of Samsun's local radio stations. In order to continue his education, he moved to Istanbul and studied Persian language and literature at Istanbul University." ,EventName = "Sagopa Kajmer", EventLocation = "Istanbul", EventDate = new DateTime(2022, 07, 19),Organizer=Organizers[1]},
                new Event() {Id=5,Descriptiton="Sercan İpekçioğlu (born 1 July 1991), better known by his stage name Ezhel (Turkish: [ezɛl]), is a Turkish rapper and singer whose music blends trap, hip hop and reggae. His song AYA has been streamed more than 80 million times on Spotify. He has been given the title Freestyle King by Hip Hop Life in Turkey." ,EventName = "Ezhel", EventLocation = "Berlin", EventDate = new DateTime(2022, 08, 1),Organizer=Organizers[0]},
                new Event() {Id=6,Descriptiton="Britney Jean Spears (born December 2, 1981) is an American singer. Often referred to as the Princess of Pop, she is credited with influencing the revival of teen pop during the late 1990s and early 2000s. Regarded as a pop icon, Spears has sold nearly 150 million records worldwide, including over 70 million solely in the United States, making her one of the world's best-selling music artists." ,EventName = "Britney Spears", EventLocation = "New York", EventDate = new DateTime(2022, 09, 21),Organizer=Organizers[0]},
                new Event() {Id=7,Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Vegan for beginners", EventLocation = "Malmo", EventDate = new DateTime(2022, 10, 25),Organizer=Organizers[0]},
                new Event() {Id=8,Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Sweden Spring Music", EventLocation = "Stockholm", EventDate = new DateTime(2022, 11, 20),Organizer=Organizers[0]},
                new Event() {Id=9,Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Miss Li", EventLocation = "Stockholm", EventDate = new DateTime(2022, 12, 1),Organizer=Organizers[0]},
            };
            MyEvents=new List<Event>();
        }
        public List<Event> GetEventsXDaysFromDate(DateTime date)
        {
            var getAllEvents = new List<Event>();
            // sortera så närmsta event är först som datetime
            Events.Sort((eventA, eventB) =>
                DateTime.Compare(eventA.ThisYearsEvent(date), eventB.ThisYearsEvent(date)));

            return getAllEvents;
        }
        public Event FindId(int id)
        {
            return Events[id];
        }
        public void JoinEvent(int id,Attendee attende)
        {
            attende.ListEvent.Add(Events[id]);
        }
    }
}
