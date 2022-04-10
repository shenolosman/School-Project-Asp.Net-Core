using System.ComponentModel.DataAnnotations;

namespace EventiaWebapp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public string Descriptiton { get; set; }
        [Required(ErrorMessage = "Please fill this field")]
        public int SeatsAvailable { get; set; }
        public User? Organizer { get; set; }
        public List<User>? Attendees { get; set; }

    }
}
