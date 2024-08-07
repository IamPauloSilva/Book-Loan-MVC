using AutoMapper;
using BookLoanApp.Data;
using BookLoanApp.Dto;
using BookLoanApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Services.BookService
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;
        private string _serverpath;
        private readonly IMapper _mapper;

        public BookService(AppDbContext context, IWebHostEnvironment system, IMapper mapper)
        {
            _context = context;
            _serverpath = system.WebRootPath;
            _mapper = mapper;
        }

        public bool CheckIfBookAlreadyExists(BookCreationDto bookCreationDto)
        {
            try
            {
                var bookDb = _context.Books.FirstOrDefault(book => book.ISBN == bookCreationDto.ISBN);

                if (bookDb != null) 
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
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

        public async Task<BooksModel> Register(BookCreationDto bookCreationDto, IFormFile foto)
        {
            try
            {
                var uniqueCode = Guid.NewGuid().ToString();
                var pathName = foto.FileName.Replace(" ", "").ToLower() + uniqueCode + bookCreationDto.ISBN + ".png";

                string imgPath = _serverpath + "\\Images\\";

                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                using (var stream = System.IO.File.Create(imgPath + pathName)) 
                { 
                    foto.CopyToAsync(stream).Wait();
                }

                //var book = new BooksModel
                //{
                //    Title = bookCreationDto.Title,
                //    Cape = pathName,
                //    Author = bookCreationDto.Author,
                //    Description = bookCreationDto.Description,
                //    StockAmount = bookCreationDto.StockAmount,
                //    PublicationYear = bookCreationDto.PublicationYear,
                //    ISBN = bookCreationDto.ISBN,
                //    Genre = bookCreationDto.Genre,

                //};

                var book = _mapper.Map<BooksModel>(bookCreationDto);
                book.Cape = pathName;

                _context.Add(book);
                await _context.SaveChangesAsync();

                return book;



            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}