using MassTransit;
using MRent.Application.Commands.Motorcycle;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Motorcycle
{
    public class UpdateMotorcycleCommandConsumer : IConsumer<UpdateMotorcycleCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcycleCommandConsumer(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Consume(ConsumeContext<UpdateMotorcycleCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var motorcycle = await _motorcycleRepository.GetByPlateAsync(command.Plate);

            if (motorcycle is null)
            {
                throw new MotorcycleNotFoundException();
            }

            motorcycle.Plate = command.Plate;
            motorcycle.Model = command.Model;
            motorcycle.Year = command.Year;
            motorcycle.Identifier = command.Identifier;

            await _motorcycleRepository.UpdateAsync(motorcycle);
        }
    }
}
