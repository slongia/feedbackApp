public class InMemoryEventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _provider;

    public InMemoryEventDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task PublishAsync<T>(T @event)
    {
        using var scope = _provider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();
        foreach (var handler in handlers)
        {
            await handler.HandleAsync(@event);
        }
    }
}