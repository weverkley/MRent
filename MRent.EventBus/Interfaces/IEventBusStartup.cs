namespace MRent.EventBus.Interfaces
{
    public interface IEventBusStartup
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
