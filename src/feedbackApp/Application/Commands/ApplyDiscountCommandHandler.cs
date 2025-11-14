public class ApplyDiscountCommandHandler : ICommandHandler<ApplyDiscountCommand>
{
    private readonly IBillingUnitOfWork _uow;
    private readonly IInvoiceCalculator _calculator;

    public ApplyDiscountCommandHandler(IBillingUnitOfWork uow, IInvoiceCalculator calculator)
    {
        _uow = uow;
        _calculator = calculator;
    }

    public async Task HandleAsync(ApplyDiscountCommand command)
    {
        var invoice = await _uow.Invoices.GetByIdAsync(command.InvoiceId)
                      ?? throw new InvalidOperationException("Invoice not found");

        await _calculator.CalculateTotalsAsync(invoice, command.DiscountCode);
        await _uow.Invoices.UpdateAsync(invoice);
        await _uow.SaveChangesAsync();
    }
}