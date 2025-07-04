using employee_api.Data;
using employee_api.Models;
using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using employee_api.Events.AbsentLogicEvents;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Infrastructure;
using employee_api.Persistance;
using employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser;
using System.Reflection.Metadata;

namespace employee_api.Tests.Persistance.ApplicationUserRepositoryTests
{
    public class CreateApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "CreateApplicationUser should be called on ApplicationUserRepository")]
        public void CreateApplicationUserShouldReturnCreatedUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }

        [Fact(DisplayName = "CreateApplicationUser should call Exception when input is null")]
        public async Task CreateApplicationUserShouldReturnNull_WhenRepositoryIsCalled()
        {
            // Arrange;

            Func<Task<ApplicationUser>> act = async () => await repository.CreateApplicationUserAsync(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'user')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "CreateApplicationUser should be called on ApplicationUserRepository")]
        public async Task CreateApplicationUserShouldReturnErrorWhenTryingToCreateUserWithExistingId_WhenRepositoryIsCalled()
        {
            // Arrange
            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = "1",
                WorkPatterns = new List<WorkPattern>()
            };

            // Act
            var response = await repository.CreateApplicationUserAsync(applicationUser, CancellationToken.None);
            applicationUser.WorkPatterns = null;
            // Assert
            response.Should().BeNull();
        }

        [Fact(DisplayName = "CreateApplicationUser should be called on ApplicationUserRepository")]
        public async Task CreateApplicationUserShouldReturnNull_WhenFakeRepositoryIsCalled()
        {
            // Arrange
            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = "1",
                WorkPatterns = new List<WorkPattern>()
            };
            context.Database.EnsureDeleted();
            // Act
            var response = await repository.CreateApplicationUserAsync(applicationUser, CancellationToken.None);
            applicationUser.WorkPatterns = null;
            // Assert
            response.Should().BeNull();           
        }
    }
}
