using Npgsql;
using Microsoft.Extensions.Configuration;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var builder = new NpgsqlConnectionStringBuilder();

        // Obtenha os valores das variáveis de ambiente
        var host = Environment.GetEnvironmentVariable("PGHOST") ?? throw new InvalidOperationException("PGHOST is not set.");
        var port = Environment.GetEnvironmentVariable("PGPORT") ?? "5432"; // valor padrão
        var username = Environment.GetEnvironmentVariable("PGUSER") ?? throw new InvalidOperationException("PGUSER is not set.");
        var password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? throw new InvalidOperationException("PGPASSWORD is not set.");
        var database = Environment.GetEnvironmentVariable("PGDATABASE") ?? throw new InvalidOperationException("PGDATABASE is not set.");

        // Construa a connection string
        builder.Host = host;
        builder.Port = int.Parse(port); // Certifique-se de que é um número válido
        builder.Username = username;
        builder.Password = password;
        builder.Database = database;
        builder.SslMode = SslMode.Require; // Ou qualquer configuração necessária
        builder.TrustServerCertificate = true; // Se necessário

        // Retorne a connection string como string
        return builder.ToString();
    }
}
