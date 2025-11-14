namespace feedbackApp.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime PaidAt { get; set; }
    public string PaymentMethod { get; set; } = default!;

    public Invoice Invoice { get; set; } = default!;
}