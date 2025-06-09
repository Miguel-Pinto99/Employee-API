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
    public class GetAllApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "GetAllApplicationUser should be called on ApplicationUserRepository")]
        public async Task GetAllApplicationUserShouldReturnGetAlldUser_WhenRepositoryIsCalled()
        {
            // Arrange

            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = 1,
                WorkPatterns = new List<WorkPattern>()
            };

            var listApplicationUsers = new List<ApplicationUser>();
            listApplicationUsers.Add(applicationUser);

            // Act
            var response = await repository.GetAllApplicationUserAsync(CancellationToken.None);

            // Assert
            response.Should().BeEquivalentTo(listApplicationUsers);
        }
    }
}
