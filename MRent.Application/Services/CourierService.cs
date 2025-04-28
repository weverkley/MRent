using AutoMapper;
using FluentValidation;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.Domain.Enums;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;
using MRent.EventBus.Interfaces;

namespace MRent.Application.Services
{
    public class CourierService : ICourierService
    {
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IValidator<CreateCourierCommandValidator> _createValidator;
        private readonly ICourierRepository _courierRepository;

        public CourierService(IMapper mapper,
            IEventBus eventBus,
            IValidator<CreateCourierCommandValidator> createValidator,
            ICourierRepository courierRepository,
            IRentRepository rentRepository)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _createValidator = createValidator;
            _courierRepository = courierRepository;
        }

        public async Task CreateAsync(CourierDTO entity)
        {
            var command = new CreateCourierCommand
            {
                Identifier = entity.identificador,
                Name = entity.nome,
                CNPJ = entity.cnpj,
                BornDate = entity.data_nascimento,
                CNH = entity.numero_cnh,
                CNHType = ConvertCNHType(entity.tipo_cnh),
            };

            var validation = await _createValidator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                throw new CourierValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        private ECNHType ConvertCNHType(string cnhType)
        {
            return cnhType switch
            {
                "A" => ECNHType.A,
                "B" => ECNHType.B,
                "AB" => ECNHType.AB,
                _ => throw new ArgumentException("Tipo de CNH inválido")
            };
        }
    }
}
