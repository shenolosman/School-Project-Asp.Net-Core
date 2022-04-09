using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly EventsHandler _eventHandler;

        public AdminController(UserManager<User> userManager, EventsHandler eventHandler)
        {
            _userManager = userManager;
            _eventHandler = eventHandler;
        }
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);

        }
        public async Task<ActionResult> EditUser(string id)
        {
            var organisatorUser = await _userManager.FindByIdAsync(id);

            return View(organisatorUser);
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(User user)
        {
            var organisatorUser = await _userManager.FindByIdAsync(user.Id);

            if (organisatorUser == null) return NotFound();
            bool userTrue = user.isOrganizer;
            if (user.isOrganizer == true)
            {
                userTrue = organisatorUser.isOrganizer = true;
                await _userManager.AddToRoleAsync(organisatorUser, "Organisator");
                await _eventHandler.ChangeStatusofUser(userTrue, organisatorUser);
                return RedirectToAction(nameof(Index));
            }
            else if (user.isOrganizer == false)
            {
                userTrue = organisatorUser.isOrganizer = false;
                await _userManager.AddToRoleAsync(organisatorUser, "Attendee");
                await _userManager.RemoveFromRoleAsync(organisatorUser, "Organisator");
                await _eventHandler.ChangeStatusofUser(userTrue, organisatorUser);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
