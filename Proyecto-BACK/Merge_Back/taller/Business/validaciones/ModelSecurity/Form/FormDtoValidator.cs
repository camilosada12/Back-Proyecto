using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Interface.ModelSecurity;
using FluentValidation;

namespace Business.validaciones.ModelSecurity.Form
{
    public class FormDtoValidator<T> : AbstractValidator<T> where T : IForm
    {
        public FormDtoValidator()
        {

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.")
                .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("El nombre no puede contener solo espacios.");

            RuleFor(x => x.description)
                .NotEmpty() .WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .Must(desc => !string.IsNullOrWhiteSpace(desc))
                .WithMessage("La descripción no puede contener solo espacios.");
        }
    }
}
