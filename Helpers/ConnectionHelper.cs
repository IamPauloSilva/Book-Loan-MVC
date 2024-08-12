using Npgsql;

namespace BookLoanApp.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            // Tenta obter a URL do banco de dados da variável de ambiente DATABASE_URL
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Se DATABASE_URL estiver disponível, construa a string de conexão a partir dela
                return BuildConnectionString(databaseUrl);
            }

            // Se DATABASE_URL não estiver definida, use a conexão padrão definida no appsettings.json
            return configuration.GetConnectionString("DefaultConnection");
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require, // Garantir a criptografia da conexão
                TrustServerCertificate = true // Pode ser necessário para aceitar certificados autoassinados
            };
            return builder.ToString();
        }
    }
}