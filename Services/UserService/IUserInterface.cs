using BookLoanApp.Dto.User;
using BookLoanApp.Models;
using static BookLoanApp.Services.UserService.UserService;

namespace BookLoanApp.Services.UserService
{
    public interface IUserInterface
    {
        Task<List<UserModel>> GetUsers(int? id);
        Task<UserCheckResult> CheckIfUserAlreadyExists(UserCreationDto userCreationDto);
        Task<UserCreationDto> Register(UserCreationDto userCreationDto);

        Task<UserModel> GetUserById(int? id);
        Task<UserModel> ChangeUserSituation(int? id);
        Task<UserModel> Edit(UserEditDto userEditDto);
    }
}
