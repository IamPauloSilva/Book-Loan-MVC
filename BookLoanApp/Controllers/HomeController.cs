using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLoanApp.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

       
        
    }
}
