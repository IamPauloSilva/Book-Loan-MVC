using BookLoanApp.Dto.User;
using BookLoanApp.Enums;
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


        [HttpGet]
        public ActionResult Register(int? id)
        {
            ViewBag.Role = ProfilesEnum.Admin;


            if (id != null)
            { 
                ViewBag.Role = ProfilesEnum.Client;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserCreationDto userCreationDto)
        {
            if (ModelState.IsValid)
            {
                if (!await _userInterface.CheckIfUserAlreadyExists(userCreationDto))
                {
                    TempData["MensagemErro"] = "User already registered!";
                    return View(userCreationDto);
                }

                var user = await _userInterface.Register(userCreationDto);
                TempData["MensagemSucesso"] = "Sucessfully registered!";

                if (user.Profile != ProfilesEnum.Client)
                {
                    return RedirectToAction("Index", "Employee");
                }

                return RedirectToAction("Index", "Client" , new { Id = "0" });

            }
            else 
            {
                TempData["MensagemErro"] = "Invalid fields!";
                return View(userCreationDto);
            }

        }
    }
}
