using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using employee_api.Application.Absent.Commands.EditAbsent;
using employee_api.Persistance;
using Xunit;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm;

namespace employee_api.Tests.Application.Absents.Commands.EditAbsent
{
    public class EditAbsentHandlerTests
    {
        private readonly Mock<IAbsentRepository> _absentRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EditAbsentHandler _handler;

        public EditAbsentHandlerTests()
        {
            _absentRepositoryMock = new Mock<IAbsentRepository>(MockBehavior.Strict);
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _handler = new EditAbsentHandler(_absentRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<EditAbsentResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call EditAbsentAsync on AbsentRepository")]
        public async Task HandleShouldCallEditAbsentAsyncOnAbsentRepository_WhenCommandIsSet()
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
                .Setup(x => x.UpdateAbsentAsync(It.IsAny<employee_api.Models.Absent>(), CancellationToken.None))
                .ReturnsAsync(absent);

            _mediatorMock.Setup(x => x.Publish(It.IsAny<AbsentLogicEvent>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new EditAbsentCommand(new Guid(), new EditAbsentCommandBody
            {
                StartDate = new DateTime(2022, 11, 29, 10, 0, 0),
                EndDate = new DateTime(2022, 11, 30, 0, 0, 0)
            });

            // Act
            EditAbsentResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Absent.Should().NotBeNull();

            _mediatorMock
                .Verify(x => x.Publish(It.IsAny<AbsentLogicEvent>(), CancellationToken.None), Times.Once);

            _absentRepositoryMock
                .Verify(x => x.UpdateAbsentAsync(It.IsAny<employee_api.Models.Absent>(), CancellationToken.None), Times.Once);
        }
    }
}
