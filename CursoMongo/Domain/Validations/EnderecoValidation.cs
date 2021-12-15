using FluentValidation;

namespace Domain.Entities
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            ValidarLogradouro();
            ValidarCidade();
            ValidarUf();
            ValidarCep();
        }

        private void ValidarCep()
        {
            RuleFor(c => c.Cep)
                 .NotEmpty().WithMessage("O campo cep não pode ser vazio.")
                 .MaximumLength(8).WithMessage("Maximo de 8 caracteres");
        }

        private void ValidarUf()
        {
            RuleFor(c => c.Uf)
                .NotEmpty().WithMessage("O campo Uf não pode ser vazio.")
                .Length(2).WithMessage("Maximo de 2 caracteres");
        }

        private void ValidarCidade()
        {
            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("O campo cidade não pode ser vazio.")
                .MaximumLength(100).WithMessage("Maximo de 100 caracteres");
        }

        private void ValidarLogradouro()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty()
                .WithMessage("Logradouro não poder ser vazio.")
                .MaximumLength(50).WithMessage("O campo logradoura não pode conter mais de 50 caracteres");
        }
    }
}