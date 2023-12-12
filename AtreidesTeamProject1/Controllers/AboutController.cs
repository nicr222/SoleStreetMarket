using Microsoft.AspNetCore.Mvc;

namespace AtreidesTeamProject1.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }
    }
}
