namespace feedbackApp.Domain.Entities;

public class BillingLog
{
    public int Id { get; set; }
    public string Message { get; set; } = default!;
    public string LogLevel { get; set; } = "Information";
    public DateTime CreatedAt { get; set; }
}