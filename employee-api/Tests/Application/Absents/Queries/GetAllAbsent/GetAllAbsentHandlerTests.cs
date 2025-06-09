using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using employee_api.Persistance;
using Xunit;
using employee_api.Models;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Application.Absent.Queries.GetAllAbsent;

namespace PVSDashboard.Tests.Application.Absents.Queries.GetAllAbsent
{
    public class GetAllAbsentHandlerTests
    {
        private readonly Mock<IAbsentRepository> _absentRepositoryMock;
        private readonly GetAllAbsentHandler _handler;

        public GetAllAbsentHandlerTests()
        {
            _absentRepositoryMock = new Mock<IAbsentRepository>(MockBehavior.Strict);
            _handler = new GetAllAbsentHandler(_absentRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<GetAllAbsentResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call GetAllAbsentAsync on AbsentRepository")]
        public async Task HandleShouldCallGetAllAbsentAsyncOnAbsentRepository_WhenCommandIsSet()
        {
            // Arrange
            var listAllAbsents = new List<employee_api.Models.Absent>
                {
                    new employee_api.Models.Absent
                    {
                        Id = Guid.NewGuid(),
                        UserId = 1,
                        StartDate = new DateTime(2022, 11, 29, 10, 0, 0),
                        EndDate = new DateTime(2022, 11, 30, 0, 0, 0)
                    }
                };
            _absentRepositoryMock
                .Setup(x => x.GetAllAbsentAsync(CancellationToken.None))
                .ReturnsAsync(listAllAbsents);

            var command = new GetAllAbsentCommand();
            // Act
            GetAllAbsentResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.ListAbsent.Should().NotBeNull();

            _absentRepositoryMock
                .Verify(x => x.GetAllAbsentAsync(CancellationToken.None), Times.Once);

        }
    }
}
