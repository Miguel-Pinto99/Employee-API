using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;
using employee_api.Persistance;
using employee_api.Events.UnsLogicEvents;
using employee_api.Application.ApplicationUsers.Commands.DeleteApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetLocation;

namespace employee_api.Tests.Application.ApplicationUsers.Commands.DeleteApplicationUser
{
    public class DeleteApplicationUserHandlerTests
    {
        private readonly Mock<IApplicationUsersRepository> _applicationUserRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly DeleteApplicationUserHandler _handler;

        public DeleteApplicationUserHandlerTests()
        {
            _applicationUserRepositoryMock = new Mock<IApplicationUsersRepository>(MockBehavior.Strict);
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _handler = new DeleteApplicationUserHandler(_applicationUserRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<DeleteApplicationUserResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call DeleteApplicationUserAsync on ApplicationUserRepository")]
        public async Task HandleShouldCallDeleteApplicationUserAsyncOnApplicationUserRepository_WhenCommandIsSet()
        {
            // Arrange
            var applicationUser = new employee_api.Models.ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = "1",
                WorkPatterns = new List<employee_api.Models.WorkPattern>()
            };

            _applicationUserRepositoryMock
                .Setup(x => x.DeleteApplicationUserAsync(1, CancellationToken.None))
                .ReturnsAsync(applicationUser);

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetApplicationUserCommand>(), CancellationToken.None))
                .ReturnsAsync(new GetApplicationUserResponse{ ApplicationUser =  applicationUser});

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLocationCommand>(), CancellationToken.None))
                .ReturnsAsync(new GetLocationResponse { UserEachLocation = new employee_api.Models.UsersEachLocation("1", new List<int> {1})});

            _mediatorMock.Setup(x => x.Publish(It.IsAny<DeleteApplicationUserLogicEvent>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new DeleteApplicationUserCommand(1);

            // Act
            DeleteApplicationUserResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.ApplicationUser.Should().NotBeNull();

            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetApplicationUserCommand>(), CancellationToken.None), Times.Once);
            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetLocationCommand>(), CancellationToken.None), Times.Once);
            _mediatorMock
                .Verify(x => x.Publish(It.IsAny<DeleteApplicationUserLogicEvent>(), CancellationToken.None), Times.Once);
            _applicationUserRepositoryMock
                .Verify(x => x.DeleteApplicationUserAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }
    }
}
