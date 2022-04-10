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
        private readonly Database _databaseService;
        private readonly SignInManager<User> _signInManager;

        public AdminController(UserManager<User> userManager, EventsHandler eventHandler, Database databaseService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _eventHandler = eventHandler;
            _databaseService = databaseService;
            _signInManager = signInManager;
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
        [ValidateAntiForgeryToken]
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
        public ViewResult DbReset()
        {
            return View();
        }
        [HttpPost, ActionName("DbReset")]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> DbResetConfirmation()
        {
            await _databaseService.RecreateAndSeed();
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
