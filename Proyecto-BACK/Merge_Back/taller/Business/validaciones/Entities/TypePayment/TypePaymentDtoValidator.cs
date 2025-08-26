using Entity.DTOs.Interface.Entities;
using FluentValidation;

namespace Business.validaciones.Entities.TypePayment
{
    public class TypePaymentDtoValidator<T> : AbstractValidator<T> where T : ITypePayment
    {
        public TypePaymentDtoValidator()
        {

            // Nombre
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("El nombre contiene caracteres inválidos.");

            // Descripción
            RuleFor(x => x.description)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s\.\,\-]+$").WithMessage("La descripción contiene caracteres inválidos.");

            // Relación con PaymentAgreement
            RuleFor(x => x.paymentAgreementId)
                .GreaterThan(0)
                .WithMessage("El campo PaymentAgreementId debe ser un número mayor que 0.");
        }
    }
}
