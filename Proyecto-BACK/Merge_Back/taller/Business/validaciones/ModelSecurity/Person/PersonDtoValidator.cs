using Entity.DTOs.Interface.ModelSecurity;
using FluentValidation;

namespace Business.Validaciones.ModelSecurity.Person
{
    public class PersonDtoValidator<T> : AbstractValidator<T> where T : IPerson
    {
        public PersonDtoValidator()
        {

            // firstName requerido y capitalizado
            RuleFor(x => x.firstName)
                .NotEmpty().WithMessage("El primer nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El primer nombre no puede superar los 100 caracteres.");

            // lastName requerido
            RuleFor(x => x.lastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(100).WithMessage("El apellido no puede superar los 100 caracteres.");


            // municipalityId válido
            RuleFor(x => x.municipalityId)
                .GreaterThan(0)
                .WithMessage("El municipio debe ser mayor a 0.");

        }
    }
}
