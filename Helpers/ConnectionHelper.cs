using Npgsql;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {

        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL" ?? "postgresql://postgres:dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA@postgres.railway.internal:5432/railway"
);
        if (!string.IsNullOrEmpty(databaseUrl))
        {
            return databaseUrl;
        }
        Console.WriteLine($"PGHOST: {Environment.GetEnvironmentVariable("PGHOST")}");
        Console.WriteLine($"PGPORT: {Environment.GetEnvironmentVariable("PGPORT")}");
        Console.WriteLine($"PGUSER: {Environment.GetEnvironmentVariable("PGUSER")}");
        Console.WriteLine($"PGDATABASE: {Environment.GetEnvironmentVariable("PGDATABASE")}");

        return BuildConnectionString(
            Environment.GetEnvironmentVariable("PGUSER") ?? "postgres",
            Environment.GetEnvironmentVariable("PGPASSWORD") ?? "dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA",
            Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway",
            Environment.GetEnvironmentVariable("PGHOST") ?? "postgres.railway.internal",
            int.Parse(Environment.GetEnvironmentVariable("PGPORT") ?? "5432")


        );
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

        Console.WriteLine($"Connection String: {builder.ToString()}");

        return builder.ToString();
    }
}
