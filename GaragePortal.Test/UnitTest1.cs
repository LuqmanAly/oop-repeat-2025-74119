using Xunit;
using GaragePortal.Domain.Entities;

namespace GaragePortal.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var admin = new Admin
            {   
                AdminName = "Test Admin",
                Email = "test@garage.com",
                Password = "password123"
            };

            // Act
            var name = admin.AdminName;
            var email = admin.Email;

            // Assert
            Assert.Equal("Test Admin", name);
            Assert.Equal("test@garage.com", email);
            Assert.NotNull(admin.Password);
        }
    }
}