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
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 05, 12)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 06, 15)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 07, 19)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 08, 1)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 09, 21)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 10, 25)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 11, 20)},
                new Event() {EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 12, 1)},
            };
        }
        public List<Event> GetBirthdaysXDaysFromDate(int days, DateTime date)
        {
            var birthdaysWithinXDays = new List<Event>();
            // sortera så närmsta event är först som datetime
            Events.Sort((birthdayA, birthdayB) =>
                DateTime.Compare(birthdayA.ThisYearsEvent(date), birthdayB.ThisYearsEvent(date)));

            return birthdaysWithinXDays;
        }
    }
}
