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
            var join = _eventHandler.GetEvents().Find(x => x.Id == id);
            return View(join);
        }
        public IActionResult Conformation(int id)
        {
            var attente = _eventHandler.GetAttendes(1);
            _eventHandler.BookEvent(attente.Id, id);
            var confirmedEvent = _eventHandler.GetEvents()
                .Find(e => e.Id == id);
            return View(confirmedEvent);
        }
    }
}
