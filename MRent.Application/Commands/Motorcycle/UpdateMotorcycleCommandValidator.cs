using FluentValidation;
using MRent.Domain.Repositories;

namespace MRent.Application.Commands.Motorcycle
{
    public class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcycleCommandValidator(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;

            RuleFor(p => p.Identifier)
                .NotEmpty().WithMessage("O identificador não pode estar vazio.");

            RuleFor(p => p.Year)
                .NotEmpty().WithMessage("O ano não pode estar vazio.");

            RuleFor(p => p.Model)
                .NotEmpty().WithMessage("O modelo não pode estar vazio.");

            RuleFor(p => p.Plate)
                .NotEmpty().WithMessage("A placa não pode estar vazia.")
                .MustAsync(IsPlateUnique)
                .WithMessage("Esta placa já está em uso.");
        }

        private async Task<bool> IsPlateUnique(string plate, CancellationToken token)
        {
            return await _motorcycleRepository.IsPlateUnique(plate);
        }
    }
}
