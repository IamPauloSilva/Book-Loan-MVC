using BookLoanApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            // Serviço: Uma instância do db context
            var dbContextSvc = svcProvider.GetRequiredService<AppDbContext>();

            // Migração: Isto é equivalente ao Update-Database
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}