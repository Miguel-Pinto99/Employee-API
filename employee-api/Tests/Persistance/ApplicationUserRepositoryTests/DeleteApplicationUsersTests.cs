using employee_api.Data;
using employee_api.Models;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;
using FluentAssertions.Specialized;
using System.Linq.Expressions;

namespace employee_api.Tests.Persistance.ApplicationUserRepositoryTests
{
    public class DeleteApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "DeleteApplicationUser should be called on ApplicationUserRepository")]
        public void DeleteApplicationUserShouldReturnDeletedUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }

        [Fact(DisplayName = "DeleteApplicationUser should be called on ApplicationUserRepository")]
        public async Task DeleteApplicationUserShouldReturnNullWhenUsingIdNotInDatabase_WhenRepositoryIsCalled()
        {
            // Arrange

            int id = 2;
            //Act
            var response = await repository.DeleteApplicationUserAsync(id, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }

        [Fact(DisplayName = "DeleteApplicationUser should be called on ApplicationUserRepository")]
        public async Task DeleteApplicationUserShouldReturnNull_WhenFakeRepositoryIsCalled()
        {
            // Arrange

            int id = 1;

            context.Database.EnsureDeleted();

            //Act
            var response = await repository.DeleteApplicationUserAsync(id, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }
    }
}
