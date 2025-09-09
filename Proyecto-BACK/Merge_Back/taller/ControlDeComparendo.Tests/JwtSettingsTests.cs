using Entity.Domain.Models.Implements.ModelSecurity;
using Xunit;

namespace ControlDeComparendo.Tests
{
    public class JwtSettingsTests
    {
        [Fact]
        public void JwtSettings_Should_Have_Default_Values()
        {
            // Arrange
            var settings = new JwtSettings();

            // Assert
            Assert.Equal("WebCDCP.API", settings.issuer);
            Assert.Equal("WebCDCP.Client", settings.audience);
            Assert.Equal(15, settings.accessTokenExpirationMinutes);
            Assert.Equal(7, settings.refreshTokenExpirationDays);
        }
    }
}

