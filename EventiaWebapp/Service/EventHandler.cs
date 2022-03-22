﻿using EventiaWebapp.Models;

namespace EventiaWebapp.Service
{
    public class EventHandler
    {
        public List<Event> Events { get; set; }

        public EventHandler()
        {
            Events = new List<Event>()
            {
                new Event() {Descriptiton="American singer-songwriter Bruno Mars has released three studio albums, one collaborative studio albums, one EP, 30 singles (seven as a featured artist) and three promotional singles. With estimated sales of over 26 million albums and 200 million singles worldwide, Mars is one of the best-selling artists of all time." , EventName = "Bruno Mars", EventLocation = "Stockholm", EventDate = new DateTime(2022, 04, 11)},
                new Event() {Descriptiton="The discography of American nu metal band Korn consists of 14 studio albums, three live albums, eight compilation albums, seven video albums, three extended plays, 45 singles, 10 promotional singles and 50 music videos." ,EventName = "Korn", EventLocation = "Malmo", EventDate = new DateTime(2022, 05, 12)},
                new Event() {Descriptiton="Metallica is an American heavy metal band. The band was formed in 1981 in Los Angeles by vocalist/guitarist James Hetfield and drummer Lars Ulrich, and has been based in San Francisco for most of its career. The band's fast tempos, instrumentals and aggressive musicianship made them one of the founding big four bands of thrash metal, alongside Megadeth, Anthrax and Slayer. " ,EventName = "Metallica", EventLocation = "Goteborg", EventDate = new DateTime(2022, 06, 15)},
                new Event() {Descriptiton="Yunus Özyavuz (born 17 August 1978), better known by his stage name Sagopa Kajmer (Turkish: [sɑgopɑ kɑʒmæɾ]), is a Turkish rapper, songwriter, record producer, and DJ. He was born in 1978 in Samsun, and finished his primary and high school there. He then started working as a DJ in one of Samsun's local radio stations. In order to continue his education, he moved to Istanbul and studied Persian language and literature at Istanbul University." ,EventName = "Sagopa Kajmer", EventLocation = "Istanbul", EventDate = new DateTime(2022, 07, 19)},
                new Event() {Descriptiton="Sercan İpekçioğlu (born 1 July 1991), better known by his stage name Ezhel (Turkish: [ezɛl]), is a Turkish rapper and singer whose music blends trap, hip hop and reggae. His song AYA has been streamed more than 80 million times on Spotify. He has been given the title Freestyle King by Hip Hop Life in Turkey." ,EventName = "Ezhel", EventLocation = "Berlin", EventDate = new DateTime(2022, 08, 1)},
                new Event() {Descriptiton="Britney Jean Spears (born December 2, 1981) is an American singer. Often referred to as the Princess of Pop, she is credited with influencing the revival of teen pop during the late 1990s and early 2000s. Regarded as a pop icon, Spears has sold nearly 150 million records worldwide, including over 70 million solely in the United States, making her one of the world's best-selling music artists." ,EventName = "Britney Spears", EventLocation = "New York", EventDate = new DateTime(2022, 09, 21)},
                new Event() {Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Vegan for beginners", EventLocation = "Malmo", EventDate = new DateTime(2022, 10, 25)},
                new Event() {Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Sweden Spring Music", EventLocation = "Stockholm", EventDate = new DateTime(2022, 11, 20)},
                new Event() {Descriptiton="Veganism is the practice of abstaining from the use of animal products, particularly in diet, and an associated philosophy that rejects the commodity status of animals.[c] An individual who follows the diet or philosophy is known as a vegan. Distinctions may be made between several categories of veganism." ,EventName = "Miss Li", EventLocation = "Stockholm", EventDate = new DateTime(2022, 12, 1)},
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
        public Event FindId(int id)
        {
            return Events[id];
        }
    }
}