using BookLoanApp.Dto.Home;
using BookLoanApp.Services.BookService;
using BookLoanApp.Services.HomeService;
using BookLoanApp.Services.SessionService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookLoanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionInterface _sessionInterface;
        private readonly IHomeInterface _homeInterface;
        private readonly IBookInterface _bookInterface;

        public HomeController(ISessionInterface sessionInterface, IHomeInterface homeInterface, IBookInterface bookInterface)
        {
            _sessionInterface = sessionInterface;
            _homeInterface = homeInterface;
            _bookInterface = bookInterface;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string search = null)
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

            if (search == null)
            {
                var booksDB = await _bookInterface.GetBooks();
                return View(booksDB);
            }
            else
            {
                var booksDB = await _bookInterface.SearchBooks(search);
                return View(booksDB);
            }
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
                if (!login.Status)
                {
                    TempData["MensagemErro"] = login.Message;
                    return View(login.Data);
                }

                if (!login.Data.Situation)
                {
                    TempData["MensagemErro"] = "Your account is inactive. Please contact support!";
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
            TempData["MensagemSucesso"] = "User logged out successfully";
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            var userSession = _sessionInterface.GetSession();

            if (userSession != null)
            {
                ViewBag.LoggedUser = userSession.Id;
                ViewBag.PageLayout = "_Layout";
            }
            else
            {
                ViewBag.PageLayout = "_Layout_NotLogged";
            }

            var book = await _bookInterface.GetBooksById(id, userSession);

            if (book.User != null)
            {
                if (book.User.LoanList == null)
                {
                    ViewBag.Loans = "No Loans";
                }
            }
            return View(book);
        }

        
        [HttpGet]
        public async Task<ActionResult> DemoAdmin()
        {
            var loginDto = new LoginDto
            {
                Email = "admin@example.com", 
                Password = "AdminPassword123!" 
            };

            return await DemoLogin(loginDto);
        }

        
        [HttpGet]
        public async Task<ActionResult> DemoClient()
        {
            var loginDto = new LoginDto
            {
                Email = "client@example.com", 
                Password = "ClientPassword123!" 
            };

            return await DemoLogin(loginDto);
        }

        
        private async Task<ActionResult> DemoLogin(LoginDto loginDto)
        {
            var login = await _homeInterface.DoLogin(loginDto);
            if (!login.Status)
            {
                TempData["MensagemErro"] = login.Message;
                return RedirectToAction("Login");
            }

            if (!login.Data.Situation)
            {
                TempData["MensagemErro"] = "The demo account is inactive. Please contact support!";
                return RedirectToAction("Login");
            }

            _sessionInterface.CreateSession(login.Data);
            TempData["MensagemSucesso"] = login.Message;

            return RedirectToAction("Index");
        }
    }
}
