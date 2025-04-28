using FluentValidation;

namespace MRent.Application.Commands.Courier
{
    public class UpdateCourierImageCNHCommandValidator : AbstractValidator<UpdateCourierImageCNHCommand>
    {
        public UpdateCourierImageCNHCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O id não pode estar vazio.");

            RuleFor(p => p.CNHImage)
                .NotEmpty().WithMessage("A imagem não pode estar vazia.");
        }
    }
}
