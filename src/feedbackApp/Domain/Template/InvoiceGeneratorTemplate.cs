public abstract class InvoiceGeneratorTemplate
{
    protected readonly IBillingUnitOfWork Uow;
    protected readonly IInvoiceFactory Factory;

    protected InvoiceGeneratorTemplate(IBillingUnitOfWork uow, IInvoiceFactory factory)
    {
        Uow = uow;
        Factory = factory;
    }

    public async Task<Invoice> GenerateAsync(int customerId,
        IEnumerable<(string desc, int qty, decimal price)> lines, string invoiceType)
    {
        Validate(lines);
        var invoice = Factory.CreateInvoice(customerId, invoiceType, lines);
        await CalculateTotalsAsync(invoice);
        await SaveInvoiceAsync(invoice);
        await LogAsync(invoice);
        return invoice;
    }

    protected abstract void Validate(IEnumerable<(string desc, int qty, decimal price)> lines);
    protected abstract Task CalculateTotalsAsync(Invoice invoice);

    protected virtual async Task SaveInvoiceAsync(Invoice invoice)
    {
        await Uow.Invoices.AddAsync(invoice);
        await Uow.SaveChangesAsync();
    }

    protected virtual Task LogAsync(Invoice invoice)
        => Task.CompletedTask;
}
