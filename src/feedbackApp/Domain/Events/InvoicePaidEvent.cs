public class InvoicePaidEvent
{
    public int InvoiceId { get; }
    public decimal Amount { get; }

    public InvoicePaidEvent(int invoiceId, decimal amount)
    {
        InvoiceId = invoiceId;
        Amount = amount;
    }
}

public interface IEventHandler<T>
{
    Task HandleAsync(T @event);
}

public interface IEventDispatcher
{
    Task PublishAsync<T>(T @event);
}