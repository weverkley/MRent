using MassTransit;
using MRent.Application.Commands.Motorcycle;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Motorcycle
{
    public class DeleteMotorcycleCommandConsumer : IConsumer<DeleteMotorcycleCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public DeleteMotorcycleCommandConsumer(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Consume(ConsumeContext<DeleteMotorcycleCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var motorcycle = await _motorcycleRepository.GetByPlateAsync(command.Plate);

            if (motorcycle is null)
            {
                throw new MotorcycleNotFoundException();
            }

            await _motorcycleRepository.RemoveAsync(motorcycle);
        }
    }
}
