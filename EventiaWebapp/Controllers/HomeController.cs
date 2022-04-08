using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
