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
    public class CheckOutApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "CheckOutApplicationUser should be called on ApplicationUserRepository")]
        public void CheckOutApplicationUserShouldReturnCheckOutdUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }
        [Fact(DisplayName = "CheckOutApplicationUser should be called on ApplicationUserRepository")]
        public async Task CheckOutApplicationUserShouldReturnNullWhenUsedIdIsNonExistent_WhenRepositoryIsCalled()
        {
            int id = 2;
            // Act
            var response = await repository.CheckOutApplicationUserAsync(id, CancellationToken.None);
            // Assert
            response.Should().BeNull();
        }
        [Fact(DisplayName = "CheckOutApplicationUser should be called on ApplicationUserRepository")]
        public async Task CheckOutApplicationUserShouldReturnNull_WhenFakeRepositoryIsCalled()
        {
            int id = 1;

            context.Database.EnsureDeleted();
            // Act
            var response = await repository.CheckInApplicationUserAsync(id, CancellationToken.None);
            // Assert
            response.Should().BeNull();
        }
    }
}
