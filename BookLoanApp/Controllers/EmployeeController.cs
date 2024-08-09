using BookLoanApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserInterface _userInterface;

        public EmployeeController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        public async Task<ActionResult> Index()
        {
            var employees = await _userInterface.GetUsers(null);
            return View(employees);
        }
    }
}
