﻿using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Event
    {
        [Key] public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Descriptiton { get; set; }
        public int SeatsAvailable { get; set; }
        public User? Organizer { get; set; }
        //public IEnumerable<User>? Attendees { get; set; }
        public List<User>? Attendees { get; set; }

    }
}
