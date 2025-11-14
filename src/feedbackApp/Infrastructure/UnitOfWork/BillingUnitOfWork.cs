// Infrastructure/UnitOfWork/BillingUnitOfWork.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
public class BillingUnitOfWork : IBillingUnitOfWork
{
    private readonly BillingDbContext _db;

    public BillingUnitOfWork(BillingDbContext db,
                             IInvoiceRepository invoices,
                             IPaymentRepository payments)
    {
        _db = db;
        Invoices = invoices;
        Payments = payments;
    }

    public IInvoiceRepository Invoices { get; }
    public IPaymentRepository Payments { get; }

    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();

    public void Dispose() => _db.Dispose();
}
