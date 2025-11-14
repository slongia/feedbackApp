using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
public interface IInvoiceCalculator
{
    Task CalculateTotalsAsync(Invoice invoice, string? discountCode);
}