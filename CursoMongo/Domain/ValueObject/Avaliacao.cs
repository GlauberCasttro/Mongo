using FluentValidation;
using FluentValidation.Results;

namespace Domain.ValueObject
{
    public class Avaliacao : AbstractValidator<Avaliacao>
    {
        public Avaliacao(int estrelas, string comentarios)
        {
            Estrelas = estrelas;
            Comentarios = comentarios;
        }

        public int Estrelas { get; private set; }
        public string Comentarios { get; private set; }
        public ValidationResult Erros { get; private set; }

        public virtual bool Validar()
        {
            ValidarEstrelas();
            ValidarComentarios();
            Erros = Validate(this);
            return Erros.IsValid;
        }

        private void ValidarEstrelas()
        {
            RuleFor(c => c.Estrelas)
                .GreaterThan(0).WithMessage("Número de estrelas deve ser maior que zero")
                .LessThanOrEqualTo(5).WithMessage("Número de estrelas deve ser menor ou igual a cinco");
        }

        private void ValidarComentarios()
        {
            RuleFor(c => c.Comentarios)
                .NotEmpty().WithMessage("Comentario não pode ser vazio")
                .MaximumLength(250).WithMessage("Comentário não pode ter mais que 150 caracteres");
        }
    }
}