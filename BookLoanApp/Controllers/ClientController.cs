using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
