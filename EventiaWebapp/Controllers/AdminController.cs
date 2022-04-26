using EventiaWebapp.Models;
using EventiaWebapp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    [Authorize(Roles = MyRole.Admin)]
    public class AdminController : Controller
    {
        private readonly AdminsHandler _adminsHandler;

        public AdminController(AdminsHandler adminsHandler)
        {
            _adminsHandler = adminsHandler;
        }
        public async Task<ActionResult> Index()
        {
            var users = await _adminsHandler.GetUsers();
            return View(users);
        }
        public async Task<ActionResult> EditUser(string id)
        {
            var organizerUser = await _adminsHandler.GetUserById(id);
            return View(organizerUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(User user)
        {
            await _adminsHandler.ChangeRole(user);
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
            await _adminsHandler.ResetDb();
            return RedirectToAction(nameof(Index));
        }
    }
}
