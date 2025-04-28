namespace MRent.EventBus.Interfaces
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent eventPayload)
            where TEvent : class;

        Task PublishAsync<TEvent>(TEvent eventPayload)
            where TEvent : class;

        void Send<TCommand>(TCommand commandPayload)
            where TCommand : class;

        void Send<TCommand>(string endpoint, TCommand commandPayload)
            where TCommand : class;
    }
}
