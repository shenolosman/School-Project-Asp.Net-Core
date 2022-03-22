using Microsoft.AspNetCore.Mvc;

namespace EventiaWebapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
