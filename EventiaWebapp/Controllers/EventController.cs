using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index(DateTime date)
        {
            return View(date);
        }

        public IActionResult MyEvent()
        {
            return View();

        }

        public IActionResult JoinEvent()
        {
            return View();
        }
    }
}
