// Infrastructure/Repositories/InvoiceRepository.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public class InvoiceRepository : IInvoiceRepository
{
    private readonly BillingDbContext _db;

    public InvoiceRepository(BillingDbContext db) => _db = db;

    public Task<Invoice?> GetByIdAsync(int id) =>
        _db.Invoices.Include(i => i.Lines).FirstOrDefaultAsync(i => i.Id == id);

    public async Task AddAsync(Invoice invoice)
    {
        await _db.Invoices.AddAsync(invoice);
    }

    public Task UpdateAsync(Invoice invoice)
    {
        _db.Invoices.Update(invoice);
        return Task.CompletedTask;
    }
}

