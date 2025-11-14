public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string LoyaltyLevel { get; set; } = "Standard";

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}