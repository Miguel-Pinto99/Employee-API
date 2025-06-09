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
    public class GetLocationsTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "GetAllLocations should be called on ApplicationUserRepository")]
        public async Task GetLocationsShouldReturnUsersInTheSameLocation_WhenRepositoryIsCalled()
        {
            // Arrange
            UsersEachLocation location = new UsersEachLocation(1, new List<int> {1});
            // Act
            var response = await repository.GetLocationAsync(1,CancellationToken.None);

            // Assert
            response.Should().BeEquivalentTo(location);
        }
        [Fact(DisplayName = "GetAllLocations should be called on ApplicationUserRepository")]
        public async Task GetAllLocationsShouldReturnCreatedUser_WhenRepositoryIsCalled()
        {
            // Arrange
            int officeLocation = 2;
            // Act
            var response = await repository.GetLocationAsync(officeLocation, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }
    }
}
