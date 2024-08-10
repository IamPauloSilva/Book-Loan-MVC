using BookLoanApp.Data;
using BookLoanApp.Dto.User;
using BookLoanApp.Models;
using BookLoanApp.Services.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookLoanApp.Services.UserService
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthenticationInterface _authenticationInterface;

        public UserService(AppDbContext dbContext,IAuthenticationInterface authenticationInterface)
        {
            _dbContext = dbContext;
            _authenticationInterface = authenticationInterface;
        }

        public async Task<bool> CheckIfUserAlreadyExists(UserCreationDto userCreationDto)
        {
            try
            {
                var checkUser = _dbContext.Users.FirstOrDefault(u => u.Email == userCreationDto.Email || u.UserName == userCreationDto.UserName);

                if (checkUser == null)
                {
                    return true;
                }
                
                return false;
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<UserModel> GetUserById(int? id)
        {
            try
            {
                var user = await _dbContext.Users.Include(e =>e.Adress).FirstOrDefaultAsync(x => x.Id == id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<UserModel>> GetUsers(int? id)
        {
            try
            {
                var userData = new List<UserModel>();

                if (id != null)
                {
                    userData= await _dbContext.Users.Where(client => client.Profile == 0).Include(e => e.Adress).ToListAsync();
                }
                else
                {
                    userData = await _dbContext.Users.Where(client => client.Profile != 0).Include(e => e.Adress).ToListAsync();
                }

                return userData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserCreationDto> Register(UserCreationDto userCreationDto)
        {
            try
            {
                _authenticationInterface.CreateHashPassword(userCreationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new UserModel
                {
                    FullName = userCreationDto.UserName,
                    UserName = userCreationDto.UserName,
                    Email = userCreationDto.Email,
                    Profile = userCreationDto.Profile,
                    Turno = userCreationDto.Turno,
                    HashPass = passwordHash,
                    SaltPass = passwordSalt,
                    
                    
                };
                var adress = new AdressModel
                {
                    StreetAdress = userCreationDto.StreetAdress,
                    DoorNumber = userCreationDto.DoorNumber,
                    City = userCreationDto.City,
                    State = userCreationDto.State,
                    Zipcode = userCreationDto.Zipcode,
                    Country = userCreationDto.Country,
                    User = user
                };

                user.Adress = adress;

                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();

                return userCreationDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
