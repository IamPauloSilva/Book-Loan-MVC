using BookLoanApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUserInterface _userInterface;

        public ClientController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var clients =await _userInterface.GetUsers(id);
            return View(clients);
        }
    }
}
