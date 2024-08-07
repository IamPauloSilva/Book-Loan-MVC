using BookLoanApp.Models;

namespace BookLoanApp.Services.BookService
{
    public interface IBookInterface
    {
        Task<List<BooksModel>> GetBooks();
    }
}