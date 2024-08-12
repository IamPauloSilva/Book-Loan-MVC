using Npgsql;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "postgresql://postgres:dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA@postgres.railway.internal:5432/railway";

        if (!string.IsNullOrEmpty(databaseUrl))
        {
            return ParseDatabaseUrl(databaseUrl);
        }

        return BuildConnectionString(
            Environment.GetEnvironmentVariable("PGUSER") ?? "postgres",
            Environment.GetEnvironmentVariable("PGPASSWORD") ?? "dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA",
            Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway",
            Environment.GetEnvironmentVariable("PGHOST") ?? "postgres.railway.internal",
            int.Parse(Environment.GetEnvironmentVariable("PGPORT") ?? "5432")
        );
    }

    private static string ParseDatabaseUrl(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = uri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        return builder.ToString();
    }

    private static string BuildConnectionString(string username, string password, string database, string host, int port)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Username = username,
            Password = password,
            Database = database,
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        return builder.ToString();
    }
}
