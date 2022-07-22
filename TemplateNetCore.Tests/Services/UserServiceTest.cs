using Shouldly;
using TemplateNetCore.Domain.Entities.Users;
using Xunit;

namespace TemplateNetCore.Tests.Services
{
    public class UserServiceTest : BaseTest
    {
        [Fact(DisplayName = "Should user not be null")]
        public void ShouldUserNotBeNull()
        {
            // Arrange
            var user = new User();
            
            // Act
            
            // Assert
            user.ShouldNotBeNull();
        }
    }
}
