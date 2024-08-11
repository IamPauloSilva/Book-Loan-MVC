using BookLoanApp.Models;

namespace BookLoanApp.Services.SessionService
{
    public interface ISessionInterface
    {

        UserModel GetSession();
        void CreateSession(UserModel userModel);
        void RemoveSession();
    }
}
