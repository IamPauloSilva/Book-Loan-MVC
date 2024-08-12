using Npgsql;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        if (string.IsNullOrEmpty(databaseUrl))
        {
            // If DATABASE_URL is not set, fallback to the default connection string
            return connectionString;
        }

        return BuildConnectionString(databaseUrl);
    }

    private static string BuildConnectionString(string databaseUrl)
    {
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        // Ensure that userInfo has both username and password
        if (userInfo.Length != 2)
        {
            throw new ArgumentException("DATABASE_URL is not in the expected format.");
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        // Optional: Log the connection string for debugging (avoid in production)
        Console.WriteLine($"Connection String: {builder.ToString()}");

        return builder.ToString();
    }
}
