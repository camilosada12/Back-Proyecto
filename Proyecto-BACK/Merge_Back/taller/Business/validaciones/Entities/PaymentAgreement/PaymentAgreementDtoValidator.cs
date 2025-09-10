using Entity.DTOs.Interface.Entities;
using FluentValidation;

namespace Business.validaciones.Entities.PaymentAgreement
{
    public class PaymentAgreementDtoValidator<T> : AbstractValidator<T> where T : IPaymentAgreement
    {
        public PaymentAgreementDtoValidator()
        {

            // Dirección (Address)
            RuleFor(x => x.address)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .Matches(@"^[a-zA-Z0-9\s\.\-\,]+$").WithMessage("La dirección contiene caracteres inválidos.");

            // Barrio (Neighborhood)
            RuleFor(x => x.neighborhood)
                .NotEmpty().WithMessage("El barrio es obligatorio.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("El barrio solo puede contener letras y espacios.");


            // Descripción del acuerdo
            RuleFor(x => x.AgreementDescription)
                .NotEmpty().WithMessage("La descripción del acuerdo es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .Matches(@"^[a-zA-Z0-9\s\.\,\-]+$").WithMessage("La descripción contiene caracteres inválidos.");

            // UserInfractionId
            RuleFor(x => x.userInfractionId)
                .GreaterThan(0).WithMessage("El campo UserInfractionId debe ser un número mayor que 0.");

            // PaymentFrequencyId
            RuleFor(x => x.paymentFrequencyId)
                .GreaterThan(0).WithMessage("El campo PaymentFrequencyId debe ser un número mayor que 0.");
        }
    }
}
