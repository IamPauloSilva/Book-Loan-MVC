using BookLoanApp.Models;

namespace BookLoanApp.Services.UserService
{
    public interface IUserInterface
    {
        Task<List<UserModel>> GetUsers(int? id);
    }
}
