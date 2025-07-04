using employee_api.Data;
using employee_api.Models;
using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;
using employee_api.Tests.Persistance.WorkPatternRepositoryTests;

namespace employee_api.Tests.Persistance.WorkPatternRepositoryTests
{
    public class GetAllWorkPatternsTests : WorkPatternRepositoryTestsBase
    {

        [Fact(DisplayName = "GetAllWorkPattern should be called on WorkPatternRepository")]
        public async Task GetAllWorkPatternShouldReturnGetAlldUser_WhenRepositoryIsCalled()
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
            var listWorkPatterns = new List<WorkPattern> { workPattern };


            // Act
            var response = await repository.GetAllWorkPatternsAsync(CancellationToken.None);

            // Assert
            response.Should().BeEquivalentTo(listWorkPatterns);
        }
    }
}
