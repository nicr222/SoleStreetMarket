using Microsoft.AspNetCore.Mvc;

namespace AtreidesTeamProject1.Controllers
{
    public class ErrorController : Controller
    {
        private readonly string connectionString;

        public ErrorController(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
