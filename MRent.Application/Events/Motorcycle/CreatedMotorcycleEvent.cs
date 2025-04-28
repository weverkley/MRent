using MRent.Domain.Events;

namespace MRent.Application.Events.Motorcycle
{
    public sealed class CreatedMotorcycleEvent : IEvent
    {
        public Guid CorrelationId { get; set; }
        public CreatedMotorcycleEvent() { }

        public CreatedMotorcycleEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
