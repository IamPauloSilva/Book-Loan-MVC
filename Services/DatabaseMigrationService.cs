using BookLoanApp.Data;
using Microsoft.EntityFrameworkCore;

public class DatabaseMigrationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Implementação do stop, se necessário
        return Task.CompletedTask;
    }
}
