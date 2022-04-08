using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly EventsHandler _eventHandler;
        private readonly UserManager<User> _userManager;

        public EventController(EventsHandler eventHandler, UserManager<User> userManager)
        {
            _eventHandler = eventHandler;
            _userManager = userManager;
        }
        [AllowAnonymous]
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
            var attenteId = _userManager.GetUserId(User);
            _eventHandler.BookEvent(attenteId, id);
            var confirmedEvent = _eventHandler.GetEvents()
                .Find(e => e.Id == id);
            return View(confirmedEvent);
        }
        //listing organisators events list
        //should add new actionresult for adding new event


    }
}
