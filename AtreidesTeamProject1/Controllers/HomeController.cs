using AtreidesTeamProject1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AtreidesTeamProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            if (TempData["LoggedInUser"] != null)
            {
                ViewBag.Username = TempData["LoggedInUser"].ToString();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//testmerge