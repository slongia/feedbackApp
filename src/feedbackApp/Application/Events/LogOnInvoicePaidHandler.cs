public class LogOnInvoicePaidHandler : IEventHandler<InvoicePaidEvent>
{
    private readonly BillingLogger _logger;
    public LogOnInvoicePaidHandler(BillingLogger logger) => _logger = logger;

    public async Task HandleAsync(InvoicePaidEvent @event)
    {
        await _logger.LogAsync($"Invoice {@event.InvoiceId} paid amount {@event.Amount}", "Information");
    }
}