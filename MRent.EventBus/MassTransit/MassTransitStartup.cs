using MassTransit;
using Microsoft.Extensions.Hosting;
using MRent.EventBus.Interfaces;

namespace MRent.EventBus.MassTransit
{
    public class MassTransitStartup : IEventBusStartup, IHostedService
    {
        private readonly IBusControl _busControl;
        private bool _started = false;

        public MassTransitStartup(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_started)
            {
                await _busControl.StartAsync(cancellationToken);
                _started = true;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_started)
            {
                await _busControl.StopAsync(cancellationToken);
                _started = false;
            }
        }
    }
}
