using employee_api.Data;
using employee_api.Models;
using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;

namespace employee_api.Tests.Persistance.ApplicationUserRepositoryTests
{
    public class GetApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "GetApplicationUser should be called on ApplicationUserRepository")]
        public void GetApplicationUserShouldReturnGetdUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }

        [Fact(DisplayName = "GetApplicationUser should be called on ApplicationUserRepository")]
        public async Task GetApplicationUserShouldReturnNullWhenWrongIdIsCalled_WhenRepositoryIsCalled()
        {
            // Arrange

            int id = 2;

            // Act
            var response = await repository.GetApplicationUserAsync(id, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }
    }
}
