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
        public async Task<IActionResult> JoinEvent(int id)
        {
            var eventet = await _eventHandler.GetEvent(id);
            return View(eventet);
        }
        public async Task<IActionResult> Confirmation(int id)
        {
            var attenteId = await _userManager.GetUserAsync(User);
            await _eventHandler.BookEvent(attenteId.Id, id);
            var confirmedEvent = await _eventHandler.GetEvent(id);
            return View(confirmedEvent);
        }
        public IActionResult OrganizersEvents()
        {
            return View();
        }
        [Authorize(Roles = "Organizer")]
        public IActionResult AddEvent()
        {
            return View();
        }
        [Authorize(Roles = "Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEvent(Event eventet)
        {
            if (ModelState.IsValid)
            {
                var eventorganisator = await _userManager.GetUserAsync(User);
                if (eventet.ImageFile != null)
                    eventet.ImageName = await _eventHandler.SaveImageFile(eventet.ImageFile);
                else
                    eventet.ImageName = "default-img.jpg";
                await _eventHandler.AddEvent(eventet, eventorganisator);
                return View(nameof(OrganizersEvents));
            }
            return View();
        }
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventet = await _eventHandler.GetEvent(id);
            if (eventet == null)
                return NotFound();
            return View(eventet);
        }
        [Authorize(Roles = "Organizer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event eventet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    eventet.ImageName = (string?)TempData["ImageName"];
                    eventet.ImageName = await _eventHandler.SaveImageFile(eventet.ImageFile, eventet.ImageName);
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

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventet = await _eventHandler.GetEvent(id);
            if (eventet == null)
                return NotFound();
            return View(eventet);
        }
        [Authorize(Roles = "Organizer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventet = await _eventHandler.GetEvent(id);
            if (eventet.ImageName != null)
                await _eventHandler.DeleteImageFile(eventet);
            await _eventHandler.DeleteEvent(eventet);
            return RedirectToAction(nameof(OrganizersEvents));
        }
        public async Task<IActionResult> DeleteMyEvent(int id)
        {
            var eventet = await _eventHandler.GetEvent(id);
            return View(eventet);
        }
        [HttpPost, ActionName("DeleteMyEvent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMyEventConfirmed(int id)
        {
            var attenteId = _userManager.GetUserId(User);
            await _eventHandler.DeleteMyEvent(attenteId, id);
            return RedirectToAction(nameof(MyEvent));
        }
    }
}
