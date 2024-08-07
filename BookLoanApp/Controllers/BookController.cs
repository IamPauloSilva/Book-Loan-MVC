using BookLoanApp.Data;
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
    }
}
