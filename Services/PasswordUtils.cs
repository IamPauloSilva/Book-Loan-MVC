using System.Security.Cryptography;

public static class PasswordUtils
{
    public static (byte[] hash, byte[] salt) CreateHashAndSalt(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }
    }
}