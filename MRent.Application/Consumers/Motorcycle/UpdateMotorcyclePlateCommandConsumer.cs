using MassTransit;
using MRent.Application.Commands.Motorcycle;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Motorcycle
{
    public class UpdateMotorcyclePlateCommandConsumer : IConsumer<UpdateMotorcyclePlateCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcyclePlateCommandConsumer(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Consume(ConsumeContext<UpdateMotorcyclePlateCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var motorcycle = await _motorcycleRepository.GetByIdAsync(command.Id);

            motorcycle.Plate = command.Plate;

            await _motorcycleRepository.UpdateAsync(motorcycle);
        }
    }
}
