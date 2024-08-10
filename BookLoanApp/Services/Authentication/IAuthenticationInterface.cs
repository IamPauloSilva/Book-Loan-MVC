namespace BookLoanApp.Services.Authentication
{
    public interface IAuthenticationInterface
    {
        public void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
