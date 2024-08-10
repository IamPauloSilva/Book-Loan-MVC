using BookLoanApp.Services.SessionService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLoanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionInterface _sessionInterface;

        public HomeController(ISessionInterface sessionInterface)
        {
            _sessionInterface = sessionInterface;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var userSession = _sessionInterface.GetSession();
            if (userSession != null)
            {
                ViewBag.PageLayout = "_Layout";
            }
            else
            {
                ViewBag.PageLayout = "_Layout_NotLogged";
            }


            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            if (_sessionInterface.GetSession() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
