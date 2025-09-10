using Entity.Domain.Models.Implements.Entities;
using Xunit;

namespace ControlDeComparendo.Tests
{
    public class TypePaymentTests
    {
        [Fact]
        public void Can_Create_TypePayment_With_Relationship()
        {
            var typePayment = new TypePayment
            {
                id = 1,
                name = "Pago en efectivo",
                paymentAgreementId = 2,
                PaymentAgreement = new PaymentAgreement { id = 2, address = "Calle 123" }
            };

            Assert.Equal(1, typePayment.id);
            Assert.Equal("Pago en efectivo", typePayment.name);
            Assert.Equal(2, typePayment.paymentAgreementId);
            Assert.NotNull(typePayment.PaymentAgreement);
        }
    }
}
