using Domain.Util;
using FluentValidation;

namespace Domain.Entities
{
    public class RestauranteValidate : AbstractValidator<Restaurante>
    {
        public RestauranteValidate()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("Nome é obrigatorio.")
                .MaximumLength(100).WithMessage("Maximo de 100 caracteres");

            RuleFor(e => e.Cozinha).NotEmpty()
                .WithMessage("Cozinha obrigatorio");

            RuleFor(e => e.Cozinha)
                .Must(EnumValidation.IsEnum).WithMessage("Cozinha não valida.");
        }
    }
}