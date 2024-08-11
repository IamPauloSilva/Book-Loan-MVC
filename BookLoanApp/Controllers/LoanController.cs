using BookLoanApp.Services.BookService;
using BookLoanApp.Services.LoanService;
using BookLoanApp.Services.SessionService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class LoanController : Controller
    {
        private readonly ISessionInterface _sessionInterface;
        private readonly IBookInterface _bookInterface;
        private readonly ILoanInterface _loanInterface;

        public LoanController(ISessionInterface sessionInterface, IBookInterface bookInterface,ILoanInterface loanInterface)
        {
            _sessionInterface = sessionInterface;
            _bookInterface = bookInterface;
            _loanInterface = loanInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Lend(int id)
        {
            var userSession = _sessionInterface.GetSession();
            if (userSession == null)
            {
                TempData["MensagemErro"] = "You need to be logged in to request book loans";
                return RedirectToAction("Login", "Home");
            }

            var loan = await _loanInterface.Lend(id);

            TempData["MensagemSucesso"] = "Loan Sucessfully Done";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Deliver(int id)
        {
            var userSession = _sessionInterface.GetSession();
            if (userSession == null)
            {
                TempData["MensagemErro"] = "You need to be logged in to request book loans";
                return RedirectToAction("Login", "Home");
            }

            var loan = await _loanInterface.Deliver(id);

            TempData["MensagemErro"] = "Book sucessfully returned";
            return RedirectToAction("Index", "Home");
        }
    }
}
