using System.Security.Cryptography;

namespace BookLoanApp.Services.Authentication
{
    public class AuthenticationService : IAuthenticationInterface
    {
        public void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
