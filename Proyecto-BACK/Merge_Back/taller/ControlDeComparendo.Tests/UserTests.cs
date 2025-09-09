using Xunit;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity; // Ajusta el namespace de tu entidad User

namespace ControlDeComparendo.Tests
{
    public class UserTests
    {
        [Fact]
        public void Can_Create_User_With_Properties()
        {
            // Arrange
            var user = new User
            {
                name = "Ingrid",
                password = "12345",
                email = "ingrid@test.com",
                PersonId = 1
            };

            // Act
            var resultName = user.name;
            var resultEmail = user.email;

            // Assert
            Assert.Equal("Ingrid", resultName);
            Assert.Equal("ingrid@test.com", resultEmail);
            Assert.Equal(1, user.PersonId);
        }

        [Fact]
        public void User_Should_Initialize_Empty_Collections()
        {
            // Arrange
            var user = new User();

            // Assert
            Assert.NotNull(user.UserInfractions);
            Assert.NotNull(user.rolUsers);
            Assert.Empty(user.UserInfractions);
            Assert.Empty(user.rolUsers);
        }
    }
}

