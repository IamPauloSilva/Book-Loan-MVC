using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
