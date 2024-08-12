using Npgsql;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Obter variáveis de ambiente diretamente
        var username = Environment.GetEnvironmentVariable("PGUSER");
        var password = Environment.GetEnvironmentVariable("PGPASSWORD");
        var database = Environment.GetEnvironmentVariable("PGDATABASE");
        var host = Environment.GetEnvironmentVariable("PGHOST");
        var portString = Environment.GetEnvironmentVariable("PGPORT");

        // Validar variáveis de ambiente
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(database) ||
            string.IsNullOrWhiteSpace(host))
        {
            Console.WriteLine("One or more environment variables are missing.");
            Console.WriteLine($"PGUSER: {username}");
            Console.WriteLine($"PGPASSWORD: {(string.IsNullOrWhiteSpace(password) ? "Not Set" : "Set")}");
            Console.WriteLine($"PGDATABASE: {database}");
            Console.WriteLine($"PGHOST: {host}");
            Console.WriteLine($"PGPORT: {portString}");
        }

        // Definir valores padrão se as variáveis de ambiente não estiverem definidas
        username = username ?? "postgres";
        password = password ?? "password";
        database = database ?? "database";
        host = host ?? "localhost";
        int port = 5432;

        if (!string.IsNullOrWhiteSpace(portString) && !int.TryParse(portString, out port))
        {
            Console.WriteLine($"Port '{portString}' is not a valid integer.");
            port = 5432; // Valor padrão
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
