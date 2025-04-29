using FluentValidation;

namespace MRent.Application.Commands.Rent
{
    public class UpdateRentCommandValidator : AbstractValidator<UpdateRentCommand>
    {
        public UpdateRentCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O id não pode estar vazio.");

            RuleFor(p => p.CourierId)
                .NotEmpty().WithMessage("O entregador id não pode estar vazio.");

            RuleFor(p => p.MotorcycleId)
                .NotEmpty().WithMessage("A moto id não pode estar vazio.");

            RuleFor(p => p.StartDate)
                .NotEmpty().WithMessage("A data inicio não pode estar vazia.");

            RuleFor(p => p.EndDate)
                .NotEmpty().WithMessage("A data termino não pode estar vazia.");

            RuleFor(p => p.ExpectedEndDate)
               .NotEmpty().WithMessage("A data previsão termino não pode estar vazia.");

            RuleFor(p => p.PlanId)
                .NotEmpty().WithMessage("O plano não pode estar vazio.");

            RuleFor(p => p.Tax)
                .NotEmpty().WithMessage("A taxa não pode estar vazia.");

            RuleFor(p => p.Subtotal)
                .NotEmpty().WithMessage("O subtotal não pode estar vazio.");

            RuleFor(p => p.Total)
                .NotEmpty().WithMessage("O total não pode estar vazio.");
        }
    }
}
