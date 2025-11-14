
// Infrastructure/Repositories/PaymentRepository.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
public class PaymentRepository : IPaymentRepository
{
    private readonly BillingDbContext _db;
    public PaymentRepository(BillingDbContext db) => _db = db;

    public async Task AddAsync(Payment payment)
    {
        await _db.Payments.AddAsync(payment);
    }
}