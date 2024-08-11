using BookLoanApp.Filters;
using BookLoanApp.Services.LoanService;
using BookLoanApp.Services.SessionService;
using BookLoanApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    [LoggedUser]
    public class ClientController : Controller
    {
        private readonly IUserInterface _userInterface;
        private readonly ISessionInterface _sessionInterface;
        private readonly ILoanInterface _loanInterface;

        public ClientController(IUserInterface userInterface, ISessionInterface sessionInterface,ILoanInterface loanInterface)
        {
            _userInterface = userInterface;
            _sessionInterface = sessionInterface;
            _loanInterface = loanInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var clients =await _userInterface.GetUsers(id);
            return View(clients);
        }

        [LoggedUserAdmin]
        public async Task<ActionResult> Profile(string search = null, string filter = "NotReturned")
        {
            var userSession = _sessionInterface.GetSession();
            if (userSession == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.Filter = filter;

            if (search != null)
            {
                var filteredLoans = await _loanInterface.GetFilteredLoans(userSession, search);
                return View(filteredLoans);
            }

            var allUserLoans = await _loanInterface.GetLoans(userSession);

            return View(allUserLoans);
        }
    }
}
