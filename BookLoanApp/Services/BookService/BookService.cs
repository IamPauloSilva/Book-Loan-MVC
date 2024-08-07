using BookLoanApp.Data;
using BookLoanApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Services.BookService
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BooksModel>> GetBooks()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                return books; // Return the list of books
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}