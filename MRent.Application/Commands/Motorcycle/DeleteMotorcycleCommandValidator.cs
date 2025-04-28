using FluentValidation;

namespace MRent.Application.Commands.Motorcycle
{
    public class DeleteMotorcycleCommandValidator : AbstractValidator<DeleteMotorcycleCommand>
    {
        public DeleteMotorcycleCommandValidator()
        {
            RuleFor(p => p.Identifier)
                .NotEmpty().WithMessage("O identificador não pode estar vazio.");

            RuleFor(p => p.Year)
                .NotEmpty().WithMessage("O ano não pode estar vazio.");

            RuleFor(p => p.Model)
                .NotEmpty().WithMessage("O modelo não pode estar vazio.");

            RuleFor(p => p.Plate)
                .NotEmpty().WithMessage("A placa não pode estar vazia.");
        }
    }
}
