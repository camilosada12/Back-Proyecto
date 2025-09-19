using System.Collections.Generic;
using Xunit;
using Entity.Domain.Models.Implements.Entities;

namespace ControlDeComparendo.Tests.Entities
{
    public class TypeInfractionTests
    {
        [Fact]
        public void Can_Create_TypeInfraction_With_Properties()
        {
            var ti = new TypeInfraction
            {
                id = 1,
                type_Infraction = "Leve",
                description = "Infracción leve",
                numer_smldv = 4 // 🔹 Nuevo campo
            };

            Assert.Equal(1, ti.id);
            Assert.Equal("Leve", ti.type_Infraction);
            Assert.Equal("Infracción leve", ti.description);
            Assert.Equal(4, ti.numer_smldv); // 🔹 Validación
        }

        [Fact]
        public void Default_Collections_State_Is_Correct()
        {
            var ti = new TypeInfraction();

            Assert.NotNull(ti.userInfractions);
            Assert.Empty(ti.userInfractions);

            Assert.NotNull(ti.fineCalculationDetail); // 🔹 ahora lo inicializaste con `new List<>();`
            Assert.Empty(ti.fineCalculationDetail);
        }

        [Fact]
        public void Can_Assign_FineCalculationDetail_After_Initialization()
        {
            var ti = new TypeInfraction
            {
                numer_smldv = 2 // 🔹 requerido
            };

            ti.fineCalculationDetail = new List<FineCalculationDetail>
            {
                new FineCalculationDetail { id = 10, formula = "SMLDV * 2", totalCalculation = 200000 }
            };

            Assert.NotNull(ti.fineCalculationDetail);
            Assert.Single(ti.fineCalculationDetail);
            Assert.Equal(10, ((List<FineCalculationDetail>)ti.fineCalculationDetail)[0].id);
        }
    }
}
