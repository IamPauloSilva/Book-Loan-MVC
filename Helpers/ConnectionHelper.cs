using Npgsql;
using Microsoft.Extensions.Configuration;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Verifique se a variável DATABASE_URL está configurada
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrEmpty(databaseUrl))
        {
            return databaseUrl;
        }

        // Fallback para configuração padrão se DATABASE_URL não estiver disponível
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
            TrustServerCertificate = true // Ajuste conforme necessário
        };

        // Para depuração: log da string de conexão gerada
        Console.WriteLine($"Connection String: {builder.ToString()}");

        return builder.ToString();
    }
}
