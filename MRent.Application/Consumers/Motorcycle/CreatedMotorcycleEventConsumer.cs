using MassTransit;
using MRent.Application.Events.Motorcycle;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Motorcycle
{
    public class CreatedMotorcycleEventConsumer : IConsumer<CreatedMotorcycleEvent>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleLogRepository _motorcycleLogRepository;

        public CreatedMotorcycleEventConsumer(IMotorcycleRepository motorcycleRepository, IMotorcycleLogRepository motorcycleLogRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _motorcycleLogRepository = motorcycleLogRepository;
        }

        public async Task Consume(ConsumeContext<CreatedMotorcycleEvent> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var motorcycle = await _motorcycleRepository.GetByIdAsync(command.CorrelationId);

            var motorcycleLog = new MotorcycleLogEntity
            {
                MotorcycleId = motorcycle.Id,
                Identifier = motorcycle.Identifier,
                Plate = motorcycle.Plate,
                Model = motorcycle.Model,
                Year = motorcycle.Year,
            };

            await _motorcycleLogRepository.AddAsync(motorcycleLog);
        }
    }
}
