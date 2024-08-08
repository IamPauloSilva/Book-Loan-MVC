using BookLoanApp.Dto;
using BookLoanApp.Models;

namespace BookLoanApp.Services.BookService
{
    public interface IBookInterface
    {
        Task<List<BooksModel>> GetBooks();

        bool CheckIfBookAlreadyExists(BookCreationDto bookCreationDto);

        Task<BooksModel> Register(BookCreationDto bookCreationDto, IFormFile foto);

        Task<BooksModel> GetBooksById(int? id);

        Task<BooksModel> Edit(BookEditDto bookEditDto, IFormFile foto);
    }
}