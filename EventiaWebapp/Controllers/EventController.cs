using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult OrganizersEvents()
        {
            return View();
        }
        [Authorize(Roles = "Organisator")]
        public IActionResult AddEvent()
        {
            return View();
        }
        [Authorize(Roles = "Organisator")]
        [HttpPost]
        public async Task<IActionResult> AddEvent(Event eventet)
        {
            if (ModelState.IsValid)
            {
                var eventorganisator = await _userManager.GetUserAsync(User);
                _eventHandler.AddEvent(eventet, eventorganisator);
                return View(nameof(OrganizersEvents));
            }
            else
            {
                return View();
            }
        }
        [Authorize(Roles = "Organisator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventet = _eventHandler.GetEvents().Find(x => x.Id == id);

            if (eventet == null)
            {
                return NotFound();
            }
            return View(eventet);
        }
        [Authorize(Roles = "Organisator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event eventet)
        {
            if (id != eventet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _eventHandler.UpdateEvent(eventet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return RedirectToAction(nameof(OrganizersEvents));
            }
            return View(eventet);
        }
        [Authorize(Roles = "Organisator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventet = _eventHandler.GetEvents().Find(x => x.Id == id);
            if (eventet == null)
            {
                return NotFound();
            }

            return View(eventet);
        }
        [Authorize(Roles = "Organisator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventet = _eventHandler.GetEvents().Find(x => x.Id == id);
            _eventHandler.DeleteEvent(eventet);
            return RedirectToAction(nameof(OrganizersEvents));
        }

        public IActionResult DeleteMyEvent(int? id)
        {
            var eventet = _eventHandler.GetEvents().Find(x => x.Id == id);
            return View(eventet);
        }
        [HttpPost, ActionName("DeleteMyEvent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMyEventConfirmed(int id)
        {
            var attenteId = _userManager.GetUserId(User);
            _eventHandler.DeleteMyEvent(attenteId, id);
            return RedirectToAction(nameof(MyEvent));
        }
    }
}
