public class Discount
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Percentage { get; set; }
    public bool IsActive { get; set; } = true;
}