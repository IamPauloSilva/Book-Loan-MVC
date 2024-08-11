namespace BookLoanApp.Services.Authentication
{
    public interface IAuthenticationInterface
    {
        public void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool CheckLogin(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
