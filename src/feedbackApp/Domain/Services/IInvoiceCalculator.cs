public interface IInvoiceCalculator
{
    Task CalculateTotalsAsync(Invoice invoice, string? discountCode);
}