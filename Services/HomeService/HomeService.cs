using BookLoanApp.Data;
using BookLoanApp.Dto.Home;
using BookLoanApp.Models;
using BookLoanApp.Services.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Services.HomeService
{
    public class HomeService : IHomeInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthenticationInterface _authenticationInterface;

        public HomeService(AppDbContext dbContext,IAuthenticationInterface authenticationInterface)
        {
            _dbContext = dbContext;
            _authenticationInterface = authenticationInterface;
        }
        public async Task<ResponseModel<UserModel>> DoLogin(LoginDto loginDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();
            try
            {
                var user =await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                if (user == null) 
                {
                    response.Data = null;
                    response.Message = "Invalid credentials!";
                    response.Status = false;

                    return response;
                }
                if(!_authenticationInterface.CheckLogin(loginDto.Password, user.HashPass, user.SaltPass))
                {
                    response.Data = null;
                    response.Message = "Invalid credentials!";
                    response.Status = false;

                    return response;
                }
                response.Data = user;
                response.Message = "Login Sucessfully!";
                

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
