using MassTransit;
using MRent.Application.Commands.Courier;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Courier
{
    public class UpdateCourierImageCNHCommandConsumer : IConsumer<UpdateCourierImageCNHCommand>
    {
        private readonly ICourierRepository _courierRepository;

        public UpdateCourierImageCNHCommandConsumer(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task Consume(ConsumeContext<UpdateCourierImageCNHCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var entity = await _courierRepository.GetByIdAsync(command.Id);

            entity.CNHImage = command.CNHImage;

            await _courierRepository.UpdateAsync(entity);
        }
    }
}
