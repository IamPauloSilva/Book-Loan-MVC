using Npgsql;

public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        // Tentar pegar a variável de ambiente DATABASE_URL
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrEmpty(databaseUrl))
        {
            Console.WriteLine($"Using DATABASE_URL: {databaseUrl}");
            return databaseUrl;
        }

        // Tentar pegar a variável de ambiente DATABASE_PUBLIC_URL
        var databasePublicUrl = Environment.GetEnvironmentVariable("DATABASE_PUBLIC_URL");
        if (!string.IsNullOrEmpty(databasePublicUrl))
        {
            Console.WriteLine($"Using DATABASE_PUBLIC_URL: {databasePublicUrl}");
            return databasePublicUrl;
        }

        // Se nenhuma das URLs estiver disponível, construir a string de conexão manualmente
        Console.WriteLine($"PGHOST: {Environment.GetEnvironmentVariable("PGHOST")}");
        Console.WriteLine($"PGPORT: {Environment.GetEnvironmentVariable("PGPORT")}");
        Console.WriteLine($"PGUSER: {Environment.GetEnvironmentVariable("PGUSER")}");
        Console.WriteLine($"PGDATABASE: {Environment.GetEnvironmentVariable("PGDATABASE")}");

        // Usar viaduct.proxy.rlwy.net:20177 como fallback
        return BuildConnectionString(
            Environment.GetEnvironmentVariable("PGUSER") ?? "postgres",
            Environment.GetEnvironmentVariable("PGPASSWORD") ?? "your_password",
            Environment.GetEnvironmentVariable("PGDATABASE") ?? "railway",
            "viaduct.proxy.rlwy.net", // Host do viaduto
            20177 // Porta do viaduto
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

        Console.WriteLine($"Connection String: {builder}");

        return builder.ToString();
    }
}

