using BookLoanApp.Models;

namespace BookLoanApp.Services.LoanService
{
    public interface ILoanInterface
    {
        Task<ResponseModel<LoanModel>> Lend(int id);

        Task<List<LoanModel>> GetFilteredLoans(UserModel userSession , string search);

        Task<List<LoanModel>> GetLoans(UserModel userSession);

        Task<LoanModel> Deliver(int id);

        Task<List<LoanModel>> GetAllLoans(string type = null);
    }
}
