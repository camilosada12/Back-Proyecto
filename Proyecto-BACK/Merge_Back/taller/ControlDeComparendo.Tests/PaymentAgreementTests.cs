using Entity.Domain.Models.Implements.Entities;
using Xunit;

namespace ControlDeComparendo.Tests
{
    public class PaymentAgreementTests
    {
        [Fact]
        public void PaymentAgreement_Should_Have_Default_Empty_Collections()
        {
            var agreement = new PaymentAgreement
            {
                id = 1,
                address = "Calle 45",
                neighborhood = "Centro",
                AgreementDescription = "Acuerdo de pago en cuotas"
            };

            Assert.Equal(1, agreement.id);
            Assert.Equal("Centro", agreement.neighborhood);
            Assert.NotNull(agreement.documentInfraction);
            Assert.Empty(agreement.documentInfraction);
            Assert.NotNull(agreement.typePayments);
            Assert.Empty(agreement.typePayments);
        }
    }
}
