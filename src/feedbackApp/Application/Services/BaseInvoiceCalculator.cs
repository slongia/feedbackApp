public class BaseInvoiceCalculator : IInvoiceCalculator
{
    private readonly BillingDbContext _db;
    private readonly IDiscountStrategy _discountStrategy;

    public BaseInvoiceCalculator(BillingDbContext db, IDiscountStrategy discountStrategy)
    {
        _db = db;
        _discountStrategy = discountStrategy;
    }

    public async Task CalculateTotalsAsync(Invoice invoice, string? discountCode)
    {
        var discount = await _discountStrategy.ApplyDiscountAsync(_db, invoice, discountCode);
        invoice.DiscountAmount = discount;
        invoice.TotalAmount = invoice.Subtotal - discount;
    }
}