using BookLoanApp.Dto.User;
using BookLoanApp.Models;

namespace BookLoanApp.Services.UserService
{
    public interface IUserInterface
    {
        Task<List<UserModel>> GetUsers(int? id);
        Task<bool> CheckIfUserAlreadyExists(UserCreationDto userCreationDto);
        Task<UserCreationDto> Register(UserCreationDto userCreationDto);

        Task<UserModel> GetUserById(int? id);
    }
}
