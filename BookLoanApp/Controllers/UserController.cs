using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
