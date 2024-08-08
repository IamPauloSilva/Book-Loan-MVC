using AutoMapper;
using BookLoanApp.Data;
using BookLoanApp.Dto;
using BookLoanApp.Models;
using BookLoanApp.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace BookLoanApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookInterface _bookInterface;
        private readonly IMapper _mapper;
        public BookController(IBookInterface bookInterface, IMapper mapper)
        {
            _bookInterface = bookInterface;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<BooksModel>>> Index()
        {
            var books = await _bookInterface.GetBooks();
            return View(books);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(BookCreationDto bookCreationDto, IFormFile foto)
        {
            if (foto != null)
            {
                if (ModelState.IsValid) 
                {
                    if (!_bookInterface.CheckIfBookAlreadyExists(bookCreationDto))
                    {
                        TempData["MensagemErro"] = "ISBN already exists!";
                        return View(bookCreationDto);
                    }

                    var book = await _bookInterface.Register(bookCreationDto, foto);

                    TempData["MensagemSucesso"] = "Book sucessfuly registered!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["MensagemErro"] = "Fill all fields!";
                    return View(bookCreationDto);
                }
            }
            else 
            {
                TempData["MensagemErro"] = "Please add a cape!";
                return View(bookCreationDto);

            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id != null)
            {
                var book =await _bookInterface.GetBooksById(id);
                return View(book);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var book = await _bookInterface.GetBooksById(id);
                var bookEditDto = _mapper.Map<BookEditDto>(book);
                return View(bookEditDto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(BookEditDto bookEditDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var book = await _bookInterface.Edit(bookEditDto, foto);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"An error occurred: {ex.Message}";
                }
            }

            TempData["MensagemErro"] = "Please check all fields!";
            return View(bookEditDto);
        }
    }
}
