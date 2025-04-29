using AutoMapper;
using MassTransit;
using MRent.Application.Commands.Rent;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Rent
{
    public class CreateRentCommandConsumer : IConsumer<CreateRentCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRentRepository _rentRepository;

        public CreateRentCommandConsumer(IMapper mapper, IRentRepository rentRepository)
        {
            _mapper = mapper;
            _rentRepository = rentRepository;
        }

        public async Task Consume(ConsumeContext<CreateRentCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var entity = _mapper.Map<RentEntity>(command);

            await _rentRepository.AddAsync(entity);
        }
    }
}
