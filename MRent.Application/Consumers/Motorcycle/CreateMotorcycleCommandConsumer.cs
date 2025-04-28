using AutoMapper;
using MassTransit;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.Events.Motorcycle;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.EventBus.Interfaces;

namespace MRent.Application.Consumers.Motorcycle
{
    public class CreateMotorcycleCommandConsumer : IConsumer<CreateMotorcycleCommand>
    {
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleCommandConsumer(IMapper mapper, IEventBus eventBus, IMotorcycleRepository motorcycleRepository)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Consume(ConsumeContext<CreateMotorcycleCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var motorcycle = _mapper.Map<MotorcycleEntity>(command);

            await _motorcycleRepository.AddAsync(motorcycle);

            if (motorcycle.Id != Guid.Empty && motorcycle.Year == 2024)
            {
                await _eventBus.PublishAsync(new CreatedMotorcycleEvent(motorcycle.Id));
            }
        }
    }
}
