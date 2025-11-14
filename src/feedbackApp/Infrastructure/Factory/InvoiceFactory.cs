// Infrastructure/Factory/InvoiceFactory.cs
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
public class InvoiceFactory : IInvoiceFactory
{
    public Invoice CreateInvoice(int customerId, string type,
        IEnumerable<(string desc, int qty, decimal price)> lines)
    {
        var invoice = new Invoice
        {
            CustomerId = customerId,
            InvoiceType = type,
            Status = "Draft",
            CreatedAt = DateTime.UtcNow
        };

        decimal subtotal = 0m;
        foreach (var l in lines)
        {
            var lineTotal = l.qty * l.price;
            invoice.Lines.Add(new InvoiceLine
            {
                Description = l.desc,
                Quantity = l.qty,
                UnitPrice = l.price,
                LineTotal = lineTotal
            });
            subtotal += lineTotal;
        }

        invoice.Subtotal = subtotal;
        invoice.TotalAmount = subtotal; // discount later
        return invoice;
    }
}