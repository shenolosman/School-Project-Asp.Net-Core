using EventiaWebapp.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventsHandler _eventHandler;

        public EventController(EventsHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyEvent()
        {
            return View();
        }
        public IActionResult JoinEvent(int id)
        {
            //_eventHandler.Events.Find(x => x.Id == id);
            return View(id);
        }
        [HttpPost]
        public IActionResult JoinEvent(int? id)
        {
            return View("Conformation", id);
        }
    }
}
