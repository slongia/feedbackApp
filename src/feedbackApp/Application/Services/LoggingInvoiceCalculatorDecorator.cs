using feedbackApp.Infrastructure.Data;
using feedbackApp.Domain.Entities;
public class LoggingInvoiceCalculatorDecorator : IInvoiceCalculator
{
    private readonly IInvoiceCalculator _inner;
    private readonly ILogger<LoggingInvoiceCalculatorDecorator> _logger;

    public LoggingInvoiceCalculatorDecorator(
        IInvoiceCalculator inner,
        ILogger<LoggingInvoiceCalculatorDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task CalculateTotalsAsync(Invoice invoice, string? discountCode)
    {
        _logger.LogInformation("Calculating invoice {InvoiceId} with discount {DiscountCode}",
            invoice.Id, discountCode);

        await _inner.CalculateTotalsAsync(invoice, discountCode);

        _logger.LogInformation("Invoice {InvoiceId} totals: Subtotal={Subtotal}, Discount={Discount}, Total={Total}",
            invoice.Id, invoice.Subtotal, invoice.DiscountAmount, invoice.TotalAmount);
    }
}