using BookLoanApp.Data;
using BookLoanApp.Models;
using BookLoanApp.Services.BookService;
using BookLoanApp.Services.SessionService;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Services.LoanService
{
    public class LoanService : ILoanInterface
    {
        private readonly IBookInterface _bookInterface;
        private readonly AppDbContext _appDbContext;
        private readonly ISessionInterface _sessionInterface;

        public LoanService(IBookInterface bookInterface, AppDbContext appDbContext, ISessionInterface sessionInterface)
        {
            _bookInterface = bookInterface;
            _appDbContext = appDbContext;
            _sessionInterface = sessionInterface;
        }
        public async Task<ResponseModel<LoanModel>> Lend(int id)
        {
            ResponseModel<LoanModel> response = new ResponseModel<LoanModel>();

            try
            {
                var userSession = _sessionInterface.GetSession();
                if (userSession == null)
                {
                    response.Status = false;
                    response.Message = "You need to login to request a book loan";
                    return response;
                }
                var book = await _bookInterface.GetBooksById(id);

                var loan = new LoanModel
                {
                    UserId = userSession.Id,
                    //User = userSession,
                    BookId = book.Id,
                    Books = book
                    
                };

                _appDbContext.Add(loan);
                await _appDbContext.SaveChangesAsync();

                var bookStock = await DecreaseStock(book);

                response.Data = loan;

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BooksModel> DecreaseStock(BooksModel book)
        {
            book.StockAmount--;
            _appDbContext.Update(book);
            await _appDbContext.SaveChangesAsync();

            return book;
        }

        public async Task<BooksModel> IncreaseStock(BooksModel book)
        {
            book.StockAmount++;
            _appDbContext.Update(book);
            await _appDbContext.SaveChangesAsync();

            return book;
        }

        public async Task<List<LoanModel>> GetFilteredLoans(UserModel userSession, string search)
        {
            try
            {
                var filteredLoans = await _appDbContext.Loans.Include(user => user.User)
                    .Include(book => book.Books)
                    .Where(loan => loan.UserId == userSession.Id
                    && loan.Books.Title.Contains(search)
                    || loan.Books.Author.Contains(search)).ToListAsync();

                return filteredLoans;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<LoanModel>> GetLoans(UserModel userSession)
        {
            try
            {
                var allUserLoans = await _appDbContext.Loans
                    .Where(loan =>loan.UserId == userSession.Id)
                    .Include(user => user.User)
                    .Include(book => book.Books).ToListAsync();


                return allUserLoans;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoanModel> Deliver(int id)
        {
            try
            {
                var loan = await _appDbContext.Loans
                    .Include(book =>book.Books)
                    .FirstOrDefaultAsync(loan => loan.Id == id);

                if (loan == null)
                {
                    throw new Exception("Loan not found");

                }

                loan.DeliverDate = DateTime.Now;

                _appDbContext.Update(loan);
                await _appDbContext.SaveChangesAsync();

                var bookStock = await IncreaseStock(loan.Books);

                return loan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LoanModel>> GetAllLoans(string type = null)
        {
            try
            {
                if (type == null)
                {
                    var returnedLoans = await _appDbContext.Loans
                        .Include(book => book.Books)
                        .Include(user =>  user.User)
                        .Where(loan => loan.DeliverDate != null).ToListAsync();
                    
                    return returnedLoans;
                }
                else
                {
                    var returnedLoans = await _appDbContext.Loans
                        .Include(book => book.Books)
                        .Include(user => user.User)
                        .Where(loan => loan.DeliverDate == null).ToListAsync();

                    return returnedLoans;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
