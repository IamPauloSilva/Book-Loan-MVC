using BookLoanApp.Data;
using BookLoanApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Services.UserService
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UserModel>> GetUsers(int? id)
        {
            try
            {
                var userData = new List<UserModel>();

                if (id != null)
                {
                    await _dbContext.Users.Where(client => client.Profile == 0).Include(e => e.Adress).ToListAsync();
                }
                else
                {
                    await _dbContext.Users.Where(client => client.Profile != 0).Include(e => e.Adress).ToListAsync();
                }

                return userData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
