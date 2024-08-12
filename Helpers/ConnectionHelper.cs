using Npgsql;
using Microsoft.Extensions.Configuration;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Tentar se conectar usando DATABASE_URL
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrEmpty(databaseUrl))
        {
            return BuildConnectionStringFromUrl(databaseUrl);
        }

        // Tentar se conectar usando DATABASE_PUBLIC_URL
        var databasePublicUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL");
        if (!string.IsNullOrEmpty(databasePublicUrl))
        {
            return BuildConnectionStringFromUrl(databasePublicUrl);
        }

        // Se nenhuma das URLs estiver disponível, tenta usar as outras variáveis
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

    // Método para construir uma connection string a partir de uma URL de conexão
    private static string BuildConnectionStringFromUrl(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = uri.AbsolutePath.TrimStart('/'),
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };

        Console.WriteLine($"Connection String from URL: {builder.ToString()}");

        return builder.ToString();
    }

    // Método para construir uma connection string a partir de parâmetros separados
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
