using MassTransit;
using MassTransit.Util;
using MRent.EventBus.Extensions;
using MRent.EventBus.Interfaces;

namespace MRent.EventBus.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IBusControl _busControl;

        public MassTransitEventBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public void Publish<TEvent>(TEvent eventPayload) where TEvent : class
        {
            TaskUtil.Await(() => _busControl.Publish(eventPayload));
        }

        public async Task PublishAsync<TEvent>(TEvent eventPayload) where TEvent : class
        {
            await _busControl.Publish(eventPayload);
        }

        public void Send<TCommand>(TCommand commandPayload) where TCommand : class
        {
            var messageUri = _busControl.DetermineQueueEndpoint(typeof(TCommand));
            TaskUtil.Await(async () =>
            {
                var sendEndpoint = await _busControl.GetSendEndpoint(messageUri);
                await sendEndpoint.Send(commandPayload);
            });
        }

        public void Send<TCommand>(string endpoint, TCommand commandPayload) where TCommand : class
        {
            TaskUtil.Await(async () =>
            {
                var sendEndpoint = await _busControl.GetSendEndpoint(new Uri(endpoint));
                await sendEndpoint.Send(commandPayload);
            });
        }
    }
}
