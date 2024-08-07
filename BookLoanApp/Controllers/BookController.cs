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
        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
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
                        return View(bookCreationDto);
                    }

                    var book = await _bookInterface.Register(bookCreationDto, foto);

                    return RedirectToAction("Index");

                }
                else
                {
                    return View(bookCreationDto);
                }
            }
            else 
            { 
                return View(bookCreationDto);

            }

            return View();
        }
    }
}
