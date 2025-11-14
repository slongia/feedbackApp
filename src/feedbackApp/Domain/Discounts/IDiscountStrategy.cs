// Domain/Discounts/IDiscountStrategy.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public interface IDiscountStrategy
{
    Task<decimal> ApplyDiscountAsync(BillingDbContext db, Invoice invoice, string? discountCode);
}

// Seasonal discount
public class SeasonalDiscountStrategy : IDiscountStrategy
{
    public async Task<decimal> ApplyDiscountAsync(BillingDbContext db, Invoice invoice, string? discountCode)
    {
        if (string.IsNullOrWhiteSpace(discountCode))
            return 0m;

        var discount = await db.Discounts
            .FirstOrDefaultAsync(d => d.Code == discountCode && d.IsActive);

        if (discount is null) return 0m;

        return invoice.Subtotal * (discount.Percentage / 100m);
    }
}

// Loyalty discount
public class LoyaltyDiscountStrategy : IDiscountStrategy
{
    public async Task<decimal> ApplyDiscountAsync(BillingDbContext db, Invoice invoice, string? discountCode)
    {
        // ignore discountCode, use loyalty
        await db.Entry(invoice).Reference(i => i.Customer).LoadAsync();

        var factor = invoice.Customer.LoyaltyLevel switch
        {
            "Gold" => 0.15m,
            "Platinum" => 0.20m,
            _ => 0m
        };

        return invoice.Subtotal * factor;
    }
}
