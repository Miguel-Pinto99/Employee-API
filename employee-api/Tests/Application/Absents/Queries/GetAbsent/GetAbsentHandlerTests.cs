using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using employee_api.Persistance;
using Xunit;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Application.Absent.Queries.GetAbsent;

namespace employee_api.Tests.Application.Absents.Queries.GetAbsent
{
    public class GetAbsentHandlerTests
    {
        private readonly Mock<IAbsentRepository> _absentRepositoryMock;
        private readonly GetAbsentHandler _handler;

        public GetAbsentHandlerTests()
        {
            _absentRepositoryMock = new Mock<IAbsentRepository>(MockBehavior.Strict);
            _handler = new GetAbsentHandler(_absentRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<GetAbsentResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call GetAbsentAsync on AbsentRepository")]
        public async Task HandleShouldCallGetAbsentAsyncOnAbsentRepository_WhenCommandIsSet()
        {
            // Arrange
            var absent = new employee_api.Models.Absent
            {
                Id = Guid.NewGuid(),
                UserId = 1,
                StartDate = new DateTime(2022, 11, 29, 10, 0, 0),
                EndDate = new DateTime(2022, 11, 30, 0, 0, 0)
            };

            _absentRepositoryMock
                .Setup(x => x.GetAbsentAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(absent);

            var command = new GetAbsentCommand(new Guid());

            // Act
            GetAbsentResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Absent.Should().NotBeNull();

            _absentRepositoryMock
                .Verify(x => x.GetAbsentAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);

        }
    }
}
