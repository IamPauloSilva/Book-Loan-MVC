using AutoMapper;
using BookLoanApp.Data;
using BookLoanApp.Dto.Book;
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
                return books; 
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
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + bookCreationDto.ISBN + ".png";

                string caminhoParaSalvarImagens = "\\app\\" +  _serverpath + "\\Images\\";

                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
                {
                    foto.CopyToAsync(stream).Wait();
                }

                var livro = _mapper.Map<BooksModel>(bookCreationDto);
                livro.Cape = nomeCaminhoImagem;


                _context.Add(livro);
                await _context.SaveChangesAsync();

                return livro;


            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BooksModel> GetBooksById(int? id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BooksModel> Edit(BookEditDto bookEditDto, IFormFile foto)
        {
            try
            {
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + bookEditDto.ISBN + ".png";

                string caminhoParaSalvarImagens = _serverpath + "\\Images\\";

                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
                {
                    foto.CopyToAsync(stream).Wait();
                }

                var livro = _mapper.Map<BooksModel>(bookEditDto);

                livro.Cape = nomeCaminhoImagem;
                livro.LastAlterationDate = DateTime.Now;


                _context.Update(livro);
                await _context.SaveChangesAsync();

                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BooksModel>> SearchBooks(string search)
        {
            try
            {
                var books = await _context.Books
                    .Where(book => book.Title.Contains(search) ||  book.Author.Contains(search)).ToListAsync();
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoanModel> GetBooksById(int? id, UserModel userSession)
        {
            try
            {
                if(userSession == null)
                {
                    var noUserLoan = await _context.Loans
                        .Include(l => l.Books)
                        .FirstOrDefaultAsync(e => e.BookId == id);

                    if(noUserLoan == null)
                    {
                        var book = await GetBooksById(id);

                        var loanDB = new LoanModel
                        {
                            Books = book,
                            User = null
                        };
                        return loanDB;
                    }
                    return noUserLoan;
                }

                var loan = await _context.Loans
                    .Include(l => l.Books)
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(e => e.BookId == id && e.DeliverDate == null && e.UserId == userSession.Id);

                if(loan == null)
                {
                    var book = await GetBooksById(id);
                    var loanDB = new LoanModel
                    {
                        Books = book,
                        User = userSession
                    };
                    return loanDB;
                }
                return loan;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}