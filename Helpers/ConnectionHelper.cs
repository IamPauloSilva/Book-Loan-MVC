using Npgsql;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Obter variáveis de ambiente diretamente
        var username = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
        var password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "password";
        var database = Environment.GetEnvironmentVariable("PGDATABASE") ?? "database";
        var host = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
        var portString = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";

        if (!int.TryParse(portString, out var port))
        {
            Console.WriteLine($"Port '{portString}' is not a valid integer.");
            port = 5432; // Default port
        }

        return BuildConnectionString(username, password, database, host, port);
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
