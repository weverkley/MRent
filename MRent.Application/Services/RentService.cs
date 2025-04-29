using AutoMapper;
using FluentValidation;
using MRent.Application.Commands.Rent;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;
using MRent.EventBus.Interfaces;

namespace MRent.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IValidator<CreateRentCommand> _createValidator;
        private readonly IValidator<UpdateRentCommand> _updateRentValidator;
        private readonly IRentRepository _rentRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ICourierRepository _courierRepository;

        public RentService(IMapper mapper,
            IEventBus eventBus,
            IValidator<CreateRentCommand> createValidator,
            IValidator<UpdateRentCommand> updateRentValidator,
            IRentRepository rentRepository,
            IPlanRepository planRepository,
            ICourierRepository courierRepository,
            IMinioService minioService)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _createValidator = createValidator;
            _updateRentValidator = updateRentValidator;
            _rentRepository = rentRepository;
            _planRepository = planRepository;
            _courierRepository = courierRepository;
        }

        public async Task<RentDTO> GetByIdAsync(Guid id)
        {
            var entity = await _rentRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new RentNotFoundException("Dados não encontrados");
            }

            return _mapper.Map<RentDTO>(entity);
        }

        public async Task CreateAsync(RentDTO entity)
        {
            var command = _mapper.Map<CreateRentCommand>(entity);

            if (entity.plano == null)
            {
                throw new RentValidationException("Plano inválido");
            }

            if (entity.data_inicio.Date <= DateTime.Now.Date)
            {
                throw new RentValidationException("Data de início inválida");
            }

            if (entity.data_termino < entity.data_inicio)
            {
                throw new RentValidationException("Data de término inválida");
            }

            var plans = await _planRepository.GetByDaysAsync(entity.plano.Value);

            if (plans.Count() == 0)
            {
                throw new RentValidationException("Plano inválido");
            }

            var courier = await _courierRepository.GetByIdAsync(command.CourierId);

            if (courier is null)
            {
                throw new RentValidationException("Entregador inválido");
            }

            if (courier.CNHType == Domain.Enums.ECNHType.B)
            {
                throw new RentValidationException("Entregador não possui habilitação");
            }

            command.PlanId = plans.ElementAt(0).Id;
            command.ExpectedEndDate = command.StartDate.AddDays(plans.ElementAt(0).Days);

            var validation = await _createValidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                throw new RentValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        public async Task UpdateReturnDateAsync(Guid id, DateTime returnDate)
        {
            var entity = await _rentRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new RentNotFoundException("Dados não encontrados");
            }

            if (returnDate.Date < entity.StartDate.Date)
            {
                throw new RentValidationException("Data de devolução inválida");
            }

            var command = _mapper.Map<UpdateRentCommand>(entity);

            var totalDays = (returnDate.Date - entity.StartDate.Date).Days;
            var totalDailies = totalDays * entity.Plan.DailyValue;

            command.ReturnDate = returnDate.Date;
            command.Subtotal = totalDailies;
            command.Total = totalDailies;

            if (returnDate.Date < entity.ExpectedEndDate.Date)
            {
                var remaining = (entity.ExpectedEndDate.Date - returnDate.Date).Days;
                var totalRemainingDailies = remaining * entity.Plan.DailyValue;

                var totalTax = totalRemainingDailies / 100 * entity.Plan.ReturnFeePercent;

                command.Tax = totalTax;
                command.Total += command.Tax;
            }

            var validation = await _updateRentValidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                throw new RentValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }
    }
}
