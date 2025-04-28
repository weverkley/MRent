using AutoMapper;
using MassTransit;
using MRent.Application.Commands.Courier;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;

namespace MRent.Application.Consumers.Courier
{
    public class CreateCourierCommandConsumer : IConsumer<CreateCourierCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICourierRepository _courierRepository;

        public CreateCourierCommandConsumer(IMapper mapper, ICourierRepository courierRepository)
        {
            _mapper = mapper;
            _courierRepository = courierRepository;
        }

        public async Task Consume(ConsumeContext<CreateCourierCommand> context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var command = context.Message;

            var entity = _mapper.Map<CourierEntity>(command);

            await _courierRepository.AddAsync(entity);
        }
    }
}
