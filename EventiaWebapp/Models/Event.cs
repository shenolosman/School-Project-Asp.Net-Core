using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventiaWebapp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int SeatsAvailable { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
        public User? Organizer { get; set; }
        public List<User>? Attendees { get; set; }

    }
}
