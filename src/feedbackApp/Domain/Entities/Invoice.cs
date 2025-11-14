namespace feedbackApp.Domain.Entities;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string InvoiceType { get; set; } = "Retail";
    public string Status { get; set; } = "Draft";
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer Customer { get; set; } = default!;
    public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}