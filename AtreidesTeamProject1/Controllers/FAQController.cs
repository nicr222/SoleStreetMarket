using Microsoft.AspNetCore.Mvc;

namespace AtreidesTeamProject1.Controllers
{
    //Controller class for the FAQ page.
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
