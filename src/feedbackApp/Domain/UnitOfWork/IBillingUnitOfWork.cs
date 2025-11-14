// Domain/UnitOfWork/IBillingUnitOfWork.cs
public interface IBillingUnitOfWork : IDisposable
{
    IInvoiceRepository Invoices { get; }
    IPaymentRepository Payments { get; }
    Task<int> SaveChangesAsync();
}
