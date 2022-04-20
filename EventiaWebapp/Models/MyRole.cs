namespace EventiaWebapp.Models
{
    public class MyRole //: IdentityRole<string>
    {
        public static string Attendee { get; set; } = "Attende";
        public string Organizer { get; set; } = "Organizer";

    }
}
