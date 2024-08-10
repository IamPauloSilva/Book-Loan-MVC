using BookLoanApp.Dto.Home;
using BookLoanApp.Models;

namespace BookLoanApp.Services.HomeService
{
    public interface IHomeInterface
    {
        Task<ResponseModel<UserModel>> DoLogin(LoginDto loginDto);
    }
}
