using employee_api.Data;
using employee_api.Models;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;
using employee_api.Tests.Persistance.WorkPatternRepositoryTests;

namespace employee_api.Tests.Persistance.WorkPatternRepositoryTests
{
    public class DeleteWorkPatternsTests : WorkPatternRepositoryTestsBase
    {
        [Fact(DisplayName = "DeleteWorkPattern should be called on WorkPatternRepository")]
        public async Task DeleteWorkPatternShouldReturnDeletedUser_WhenRepositoryIsCalled()
        {
            // Arrange

            Guid id = userInDb.Id;
            var workPattern = new WorkPattern
            {
                Id = id,
                UserId = 1,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2023, 1, 1, 0, 0, 0),
                Parts = new List<WorkPatternPart>()
            };


            //Act
            var response = await repository.DeleteWorkPatternAsync(id, CancellationToken.None);
            var newWorkPatternInDb = context.WorkPattern.FirstOrDefaultAsync(x => x.Id == workPattern.Id);
            // Assert
            newWorkPatternInDb.Result.Should().BeNull();
            response.Should().BeEquivalentTo(workPattern);
        }

        [Fact(DisplayName = "DeleteWorkPattern should be called on WorkPatternRepository")]
        public async Task DeleteWorkPatternShouldReturnNullWhenUsingIdNotInDatabase_WhenRepositoryIsCalled()
        {
            // Arrange

            var workPattern = new WorkPattern
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                UserId = 1,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2023, 1, 1, 0, 0, 0),
            };
            //Act
            var response = await repository.DeleteWorkPatternAsync(workPattern.Id, CancellationToken.None);

            // Assert
            response.Should().BeNull();
        }

        [Fact(DisplayName = "DeleteWorkPattern should be called on WorkPatternRepository")]
        public async Task DeleteWorkPatternShouldReturnNull_WhenFakeRepositoryIsCalled()
        {
            // Arrange

            Guid id = userInDb.Id;
            var workPattern = new WorkPattern
            {
                Id = id,
                UserId = 1,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2023, 1, 1, 0, 0, 0),
                Parts = new List<WorkPatternPart>()
            };

            context.Database.EnsureDeleted();
            //Act
            var response = await repository.DeleteWorkPatternAsync(id, CancellationToken.None);
            // Assert
            response.Should().BeNull();
        }
    }
}
