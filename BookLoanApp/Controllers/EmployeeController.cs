using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
