using FluentValidation;
using MRent.Domain.Repositories;

namespace MRent.Application.Commands.Motorcycle
{
    public class UpdateMotorcyclePlateCommandValidator : AbstractValidator<UpdateMotorcyclePlateCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcyclePlateCommandValidator(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O id não pode estar vazio.");

            RuleFor(p => p.Plate)
                .NotEmpty().WithMessage("A placa não pode estar vazia.")
                .MustAsync(IsPlateUnique)
                .WithMessage("Esta placa já está em uso.");
        }

        private async Task<bool> IsPlateUnique(string plate, CancellationToken token)
        {
            return await _motorcycleRepository.IsPlateUniqueAsync(plate);
        }
    }
}
