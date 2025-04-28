using FluentValidation;
using MRent.Domain.Repositories;

namespace MRent.Application.Commands.Motorcycle
{
    public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
    {
        private readonly ICourierRepository _courierRepository;

        public CreateCourierCommandValidator(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;

            RuleFor(p => p.Identifier)
                .NotEmpty().WithMessage("O identificador não pode estar vazio.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome não pode estar vazio.");

            RuleFor(p => p.CNPJ)
                .NotEmpty().WithMessage("O cnpj não pode estar vazio.");

            RuleFor(p => p.BornDate)
                .NotEmpty().WithMessage("A data de nascimento não pode estar vazia.");

            RuleFor(p => p.CNH)
                .NotEmpty().WithMessage("O número da cnh não pode estar vazio.")
                .MustAsync(IsCNPJUnique)
                .WithMessage("Esta placa já está em uso.");

            RuleFor(p => p.CNHType)
                .NotEmpty().WithMessage("O tipo de cnh não pode estar vazio.");
        }

        private async Task<bool> IsCNPJUnique(string cnpj, CancellationToken token)
        {
            return await _courierRepository.IsCNPJUniqueAsync(cnpj);
        }
    }
}
