using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;
using employee_api.Application.ApplicationUsers.Commands.EditApplicationUser;
using employee_api.Persistance;
using employee_api.Events.UnsLogicEvents;
using employee_api.Application.ApplicationUsers.Queries.EditApplicationUser;

namespace employee_api.Tests.Application.ApplicationUsers.Commands.EditApplicationUser
{
    public class EditApplicationUserHandlerTests
    {
        private readonly Mock<IApplicationUsersRepository> _applicationUserRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EditApplicationUserHandler _handler;

        public EditApplicationUserHandlerTests()
        {
            _applicationUserRepositoryMock = new Mock<IApplicationUsersRepository>(MockBehavior.Strict);
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _handler = new EditApplicationUserHandler(_applicationUserRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<EditApplicationUserResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call EditApplicationUserAsync on ApplicationUserRepository")]
        public async Task HandleShouldCallEditApplicationUserAsyncOnApplicationUserRepository_WhenCommandIsSet()
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
                .Setup(x => x.GetApplicationUserAsync(It.IsAny<int>(),false, CancellationToken.None))
                .ReturnsAsync(applicationUser);

            _applicationUserRepositoryMock
                .Setup(x => x.UpdateApplicationUserAsync(It.IsAny<employee_api.Models.ApplicationUser>(), CancellationToken.None))
                .ReturnsAsync(applicationUser);

            _mediatorMock.Setup(x => x.Publish(It.IsAny<EditApplicationUserLogicEvent>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new EditApplicationUserCommand(1, new EditApplicationUserCommandBody
            {
                FirstName = "Miguel",
                OfficeLocation = "1"
            });

            // Act
            EditApplicationUserResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.ApplicationUser.Should().NotBeNull();

            _mediatorMock
                .Verify(x => x.Publish(It.IsAny<EditApplicationUserLogicEvent>(), CancellationToken.None), Times.Once);
            _applicationUserRepositoryMock
                .Verify(x => x.UpdateApplicationUserAsync(It.IsAny<employee_api.Models.ApplicationUser>(), CancellationToken.None), Times.Once);
            _applicationUserRepositoryMock
                .Verify(x => x.GetApplicationUserAsync(It.IsAny<int>(),false, CancellationToken.None), Times.Once);
        }
    }
}
