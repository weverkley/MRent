using AutoMapper;
using FluentValidation;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.DTO;
using MRent.Application.Interfaces;
using MRent.Domain.Exceptions;
using MRent.Domain.Repositories;
using MRent.EventBus.Interfaces;

namespace MRent.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IValidator<CreateMotorcycleCommand> _createValidator;
        private readonly IValidator<UpdateMotorcyclePlateCommand> _updateValidator;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentRepository _rentRepository;

        public MotorcycleService(IMapper mapper,
            IEventBus eventBus,
            IValidator<CreateMotorcycleCommand> createValidator,
            IValidator<UpdateMotorcyclePlateCommand> updateValidator,
            IMotorcycleRepository motorcycleRepository,
            IRentRepository rentRepository)
        {
            _mapper = mapper;
            _eventBus = eventBus;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _motorcycleRepository = motorcycleRepository;
            _rentRepository = rentRepository;
        }

        public async Task<IEnumerable<MotorcycleDTO>> GetAllAsync()
        {
            var motorcycles = await _motorcycleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MotorcycleDTO>>(motorcycles);
        }

        public async Task<MotorcycleDTO> GetByIdAsync(Guid id)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);
            if (motorcycle == null) throw new MotorcycleNotFoundException();

            return _mapper.Map<MotorcycleDTO>(motorcycle);
        }

        public async Task<MotorcycleDTO> GetByPlateAsync(string plate)
        {
            var motorcycle = await _motorcycleRepository.GetByPlateAsync(plate);
            if (motorcycle == null) throw new MotorcycleNotFoundException();

            return _mapper.Map<MotorcycleDTO>(motorcycle);
        }

        public async Task<MotorcycleDTO> GetByIdentifierAsync(string identifier)
        {
            var motorcycle = await _motorcycleRepository.GetByIdentifierAsync(identifier);
            if (motorcycle == null) throw new MotorcycleNotFoundException();

            return _mapper.Map<MotorcycleDTO>(motorcycle);
        }

        public async Task CreateAsync(MotorcycleDTO entity)
        {
            var command = new CreateMotorcycleCommand
            {
                Identifier = entity.identificador,
                Year = entity.ano,
                Model = entity.modelo,
                Plate = entity.placa
            };

            var validation = await _createValidator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                throw new MotorcycleValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        public async Task UpdatePlateAsync(Guid id, string plate)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);
            if (motorcycle == null) throw new MotorcycleNotFoundException();

            var command = new UpdateMotorcyclePlateCommand
            {
                Id = id,
                Plate = plate
            };

            var validation = await _updateValidator.ValidateAsync(command);

            if (!validation.IsValid)
            {
                throw new MotorcycleValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            var command = new DeleteMotorcycleCommand
            {
                Id = id
            };

            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);

            if (motorcycle == null) throw new MotorcycleNotFoundException();

            var rents = await _rentRepository.GetByMotorcycleIdAsync(motorcycle.Id);

            if (rents.Any())
            {
                throw new MotorcycleValidationException("Dados inválidos");
            }

            await _eventBus.PublishAsync(command);
        }
    }
}
