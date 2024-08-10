using AutoMapper;
using BookLoanApp.Dto.Adress;
using BookLoanApp.Dto.User;
using BookLoanApp.Enums;
using BookLoanApp.Models;
using BookLoanApp.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserInterface _userInterface;
        private readonly IMapper _mapper;

        public UserController(IUserInterface userInterface, IMapper mapper)
        {
            _userInterface = userInterface;
            _mapper = mapper;
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
                ViewBag.ID = id;

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

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {

            if (id != null)
            {
                var user = await _userInterface.GetUserById(id);
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ChangeUserSituation(UserModel userModel)
        {
            if (userModel.Id != 0 && userModel.Id != null)
            {
                var userDb = await _userInterface.ChangeUserSituation(userModel.Id);


                if (userDb.Situation == true)
                {
                    TempData["MensagemSucesso"] = "User sucessfully activated!";
                }
                else
                {
                    TempData["MensagemSucesso"] = "User sucessfully inactivated!";
                }

                if (userDb.Profile != ProfilesEnum.Client)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Client", new { Id = "0" });
                }


            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id != null)
            {
                var user = await _userInterface.GetUserById(Id);

                var userEdited = new UserEditDto
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Profile = user.Profile,
                    Turno = user.Turno,
                    Id = user.Id,
                    UserName = user.UserName,
                    Adress = _mapper.Map<AdressEditDto>(user.Adress)
                };

                if (userEdited.Profile == ProfilesEnum.Client)
                {
                    ViewBag.Role = ProfilesEnum.Client;
                }
                else
                {
                    ViewBag.Role = ProfilesEnum.Admin;
                }

                return View(userEdited);
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<ActionResult> Edit(UserEditDto userEditDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userInterface.Edit(userEditDto);
                TempData["MensagemSucesso"] = "Sucessfully Edited";

                if (user.Profile != ProfilesEnum.Client)
                {
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Client",new {Id="0"});
                }
            }
            else
            {
                TempData["MensagemErro"] = "Check provided data";
                return View(userEditDto);
            }
        }

    }

    
}
