// Infrastructure/Logging/BillingLogger.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
public sealed class BillingLogger
{
    private readonly BillingDbContext _dbContext;

    // DI singleton – but still a classic "single instance" object
    public BillingLogger(BillingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogAsync(string message, string level = "Information")
    {
        var log = new BillingLog
        {
            Message = message,
            LogLevel = level,
            CreatedAt = DateTime.UtcNow
        };
        await _dbContext.BillingLogs.AddAsync(log);
        await _dbContext.SaveChangesAsync();
    }
}
