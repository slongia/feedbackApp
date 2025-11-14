using feedbackApp.Infrastructure.Data;
using feedbackApp.Domain.Entities;
public class OnlineInvoiceGenerator : InvoiceGeneratorTemplate
{
    private readonly IInvoiceCalculator _calculator;

    public OnlineInvoiceGenerator(
        IBillingUnitOfWork uow,
        IInvoiceFactory factory,
        IInvoiceCalculator calculator)
        : base(uow, factory)
    {
        _calculator = calculator;
    }

    protected override void Validate(IEnumerable<(string desc, int qty, decimal price)> lines)
    {
        if (!lines.Any())
            throw new InvalidOperationException("Invoice must have at least one line");
    }

    protected override async Task CalculateTotalsAsync(Invoice invoice)
    {
        await _calculator.CalculateTotalsAsync(invoice, null);
    }
}