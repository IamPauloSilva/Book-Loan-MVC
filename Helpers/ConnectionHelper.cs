using Npgsql;
using System;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Para usar a string de conexão explícita, defina-a diretamente aqui
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        // Verifica se a variável de ambiente DATABASE_URL está configurada corretamente
        if (!string.IsNullOrEmpty(databaseUrl))
        {
            // Utilize a string de conexão fornecida pela variável de ambiente
            return databaseUrl;
        }

        // Se DATABASE_URL não estiver definida, use uma string de conexão padrão
        // (apenas para desenvolvimento local ou fallback)
        return BuildConnectionString(
            "postgres", // Usuário
            "dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA", // Senha
            "railway", // Banco de dados
            "postgres.railway.internal", // Host
            5432 // Porta
        );
    }

    // Método opcional para construir uma string de conexão a partir de parâmetros explícitos
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
            TrustServerCertificate = true // Ajuste conforme necessário para sua configuração de produção
        };

        return builder.ToString();
    }
}
