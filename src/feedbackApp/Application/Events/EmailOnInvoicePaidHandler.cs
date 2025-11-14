public class EmailOnInvoicePaidHandler : IEventHandler<InvoicePaidEvent>
{
    public Task HandleAsync(InvoicePaidEvent @event)
    {
        // fake email
        Console.WriteLine($"[EMAIL] Invoice {@event.InvoiceId} paid {@event.Amount}");
        return Task.CompletedTask;
    }
}