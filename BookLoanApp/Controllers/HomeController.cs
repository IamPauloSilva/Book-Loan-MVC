using BookLoanApp.Dto.Home;
using BookLoanApp.Services.HomeService;
using BookLoanApp.Services.SessionService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookLoanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionInterface _sessionInterface;
        private readonly IHomeInterface _homeInterface;

        public HomeController(ISessionInterface sessionInterface, IHomeInterface homeInterface)
        {
            _sessionInterface = sessionInterface;
            _homeInterface = homeInterface;
        }

        public IHomeInterface HomeInterface { get; }

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
        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var login = await _homeInterface.DoLogin(loginDto);
                if (login.Status == false)
                {
                    TempData["MensagemErro"] = login.Message;
                    return View(login.Data);
                }

                if (login.Data.Situation == false)
                {
                    TempData["MensagemErro"] = "Your account is inactive. please contact support!";
                    return View("Login");
                }

                _sessionInterface.CreateSession(login.Data);
                TempData["MensagemSucesso"] = login.Message;

                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout() 
        {
            _sessionInterface.RemoveSession();
            TempData["MensagemSucesso"] = "User logged out sucessfully";
            return RedirectToAction("Login", "Home");
        }
    }
}
