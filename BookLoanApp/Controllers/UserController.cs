using BookLoanApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var users = await _userInterface.GetUsers(id);
            return View(users);
        }
    }
}
