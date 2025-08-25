using Entity.DTOs.Interface;
using FluentValidation;

namespace Business.validaciones.DocumentInfraction
{
    public class DocumentInfractionDtoValidator<T> : AbstractValidator<T>
        where T : IDocumentInfraction
    {
        public DocumentInfractionDtoValidator()
        {

            RuleFor(d => d.inspectoraReportId)
                .GreaterThan(0)
                .WithMessage("El campo InspectoraReportId debe ser un número mayor que 0.");

            RuleFor(d => d.PaymentAgreementId)
                .GreaterThan(0)
                .WithMessage("El campo PaymentAgreementId debe ser un número mayor que 0.");
        }
    }
}
