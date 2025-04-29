using FluentValidation;
using MRent.Domain.Repositories;

namespace MRent.Application.Commands.Rent
{
    public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
    {
        private readonly IRentRepository _rentRepository;

        public CreateRentCommandValidator(IRentRepository rentRepository)
        {
            _rentRepository = rentRepository;

            RuleFor(p => p.CourierId)
                .NotEmpty().WithMessage("O entregador id não pode estar vazio.");

            RuleFor(p => p.MotorcycleId)
                .NotEmpty().WithMessage("A moto id não pode estar vazio.")
                .MustAsync(IsMotorcycleFree).WithMessage("A moto não pode estar alugada.");

            RuleFor(p => p.StartDate)
                .NotEmpty().WithMessage("A data inicio não pode estar vazia.");

            RuleFor(p => p.EndDate)
                .NotEmpty().WithMessage("A data termino não pode estar vazia.");

            RuleFor(p => p.ExpectedEndDate)
               .NotEmpty().WithMessage("A data previsão termino não pode estar vazia.");

            RuleFor(p => p.PlanId)
                .NotEmpty().WithMessage("O plano não pode estar vazio.");
        }

        private async Task<bool> IsMotorcycleFree(Guid motorcycleId, CancellationToken token)
        {
            var motorcyle = await _rentRepository.GetByMotorcycleIdAsync(motorcycleId);
            return motorcyle.Count() == 0;
        }
    }
}
