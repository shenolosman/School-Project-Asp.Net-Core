using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private List<EventHandler> _eventHandlers;
        public IActionResult Index(DateTime date)
        {
            return View(date);
        }

        public IActionResult MyEvent()
        {
            return View();
        }

        public IActionResult JoinEvent(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult JoinEvent(int? id)
        {
            return RedirectToAction("Conformation");
        }

        [HttpPost]
        public IActionResult Conformation(int? id)
        {
            return View("MyEvent");
        }
    }
}
