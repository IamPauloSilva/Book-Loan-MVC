using AutoMapper;
using BookLoanApp.Data;
using BookLoanApp.Dto.Adress;
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
        private readonly IMapper _mapper;

        public UserService(AppDbContext dbContext,IAuthenticationInterface authenticationInterface, IMapper mapper)
        {
            _dbContext = dbContext;
            _authenticationInterface = authenticationInterface;
            _mapper = mapper;
        }

        public async Task<UserModel> ChangeUserSituation(int? id)
        {
            try
            {
                var userChangeSituation = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (userChangeSituation != null)
                {
                    if (userChangeSituation.Situation == true)
                    {
                        userChangeSituation.Situation = false;
                        userChangeSituation.LastAlterationDate = DateTime.Now;

                    }
                    else
                    {
                        userChangeSituation.Situation = true;
                        userChangeSituation.LastAlterationDate = DateTime.Now;
                    }

                    _dbContext.Update(userChangeSituation);
                    await _dbContext.SaveChangesAsync();

                    return userChangeSituation;
                }

                return userChangeSituation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<UserModel> Edit(UserEditDto userEditDto)
        {
            try
            {
                var userDB = await _dbContext.Users.Include(e => e.Adress).FirstOrDefaultAsync(u => u.Id == userEditDto.Id);
                if (userDB != null)
                {
                    userDB.Turno = userEditDto.Turno;
                    userDB.Profile = userEditDto.Profile;
                    userDB.FullName = userEditDto.FullName;
                    userDB.UserName = userEditDto.UserName;
                    userDB.Email = userEditDto.Email;
                    userDB.LastAlterationDate = DateTime.Now;
                    userDB.Adress = _mapper.Map<AdressModel>(userEditDto.Adress);

                    _dbContext.Update(userDB);
                    await _dbContext.SaveChangesAsync();

                    return userDB;
                }
                return userDB;

            }
            catch (Exception ex)
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
