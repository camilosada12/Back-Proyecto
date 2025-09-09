using System.Collections.Generic;
using Xunit;
using Entity.Domain.Models.Implements.Entities; // Ajusta este namespace si tu proyecto usa otro

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
                numer_smldv = 2,
                description = "Infracción leve"
            };

            Assert.Equal(1, ti.id);
            Assert.Equal("Leve", ti.type_Infraction);
            Assert.Equal(2, ti.numer_smldv);
            Assert.Equal("Infracción leve", ti.description);
        }

        [Fact]
        public void Default_Collections_State_Is_Correct()
        {
            var ti = new TypeInfraction();

            // En tu clase userInfractions está inicializada -> no debe ser null y debe estar vacía
            Assert.NotNull(ti.userInfractions);
            Assert.Empty(ti.userInfractions);

            // fineCalculationDetail NO está inicializada en la clase que pegaste -> por defecto es null
            Assert.Null(ti.fineCalculationDetail);
        }

        [Fact]
        public void Can_Assign_FineCalculationDetail_After_Initialization()
        {
            var ti = new TypeInfraction();

            // Simular inicialización manual (lo que haría EF al cargar relaciones, por ejemplo)
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

