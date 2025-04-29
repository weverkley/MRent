using AutoMapper;
using MassTransit;
using MRent.Application.Commands.Rent;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Rent
{
    public class UpdateRentCommandConsumer : IConsumer<UpdateRentCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRentRepository _rentRepository;

        public UpdateRentCommandConsumer(IMapper mapper, IRentRepository rentRepository)
        {
            _mapper = mapper;
            _rentRepository = rentRepository;
        }

        public async Task Consume(ConsumeContext<UpdateRentCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var entity = await _rentRepository.GetByIdAsync(command.Id);

            entity.ExpectedEndDate = command.ExpectedEndDate;
            entity.Tax = command.Tax;
            entity.Subtotal = command.Subtotal;
            entity.Total = command.Total;

            await _rentRepository.UpdateAsync(entity);
        }
    }
}
