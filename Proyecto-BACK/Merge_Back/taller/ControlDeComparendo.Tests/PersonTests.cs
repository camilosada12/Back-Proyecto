using Xunit;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;

namespace ControlDeComparendo.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Can_Create_Person_With_Properties()
        {
            // Arrange
            var person = new Person
            {
                firstName = "Ingrid",
                lastName = "Medina",
                phoneNumber = "123456",
                address = "Calle 123",
                documentTypeId = 1,
                municipalityId = 2
            };

            // Act & Assert
            Assert.Equal("Ingrid", person.firstName);
            Assert.Equal("Medina", person.lastName);
            Assert.Equal("123456", person.phoneNumber);
            Assert.Equal("Calle 123", person.address);
            Assert.Equal(1, person.documentTypeId);
            Assert.Equal(2, person.municipalityId);
        }

        [Fact]
        public void Person_Should_Allow_Navigation_To_User()
        {
            // Arrange
            var user = new User { name = "Ingrid" };
            var person = new Person { User = user };

            // Assert
            Assert.NotNull(person.User);
            Assert.Equal("Ingrid", person.User.name);
        }
    }
}
