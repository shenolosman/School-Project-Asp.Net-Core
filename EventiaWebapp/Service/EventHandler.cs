using EventiaWebapp.Models;

namespace EventiaWebapp.Service
{
    public class EventHandler
    {
        public List<Event> Events { get; set; }

        public EventHandler()
        {
            Events = new List<Event>()
            {
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 04, 11)},
                new Event() {EventName = "Korn", EventLocation = "Malmo", EventDate = new DateTime(2022, 05, 12)},
                new Event() {EventName = "Metallica", EventLocation = "Goteborg", EventDate = new DateTime(2022, 06, 15)},
                new Event() {EventName = "Sagopa Kajmer", EventLocation = "Istanbul", EventDate = new DateTime(2022, 07, 19)},
                new Event() {EventName = "Ezhel", EventLocation = "Berlin", EventDate = new DateTime(2022, 08, 1)},
                new Event() {EventName = "Britney Spears", EventLocation = "New York", EventDate = new DateTime(2022, 09, 21)},
                new Event() {EventName = "Vegan for beginners", EventLocation = "Malmo", EventDate = new DateTime(2022, 10, 25)},
                new Event() {EventName = "Sweden Spring Music", EventLocation = "Stockholm", EventDate = new DateTime(2022, 11, 20)},
                new Event() {EventName = "Miss Li", EventLocation = "Stockholm", EventDate = new DateTime(2022, 12, 1)},
            };
        }
        public List<Event> GetBirthdaysXDaysFromDate(DateTime date)
        {
            var getAllEvents = new List<Event>();
            // sortera så närmsta event är först som datetime
            Events.Sort((eventA, eventB) =>
                DateTime.Compare(eventA.ThisYearsEvent(date), eventB.ThisYearsEvent(date)));

            return getAllEvents;
        }
    }
}
