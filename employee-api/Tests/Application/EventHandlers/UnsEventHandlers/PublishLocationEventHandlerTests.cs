using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using employee_api.Application.Uns.EventHandlers;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;
using employee_api.Timers;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace employee_api.Tests.Application.EventHandlers.UnsEventHandlers
{
    public class PublishLocationEventHandlerTests
    {
        private readonly Mock<IUnsService> _unsServiceMock;
        private readonly Mock<ITimerService> _timerServiceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PublishLocationEventHandler _handler;

        public PublishLocationEventHandlerTests()
        {
            _unsServiceMock = new Mock<IUnsService>(MockBehavior.Strict);
            _handler = new PublishLocationEventHandler(_unsServiceMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            //Arrange

            Func<Task> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'notification')");
            //Act

            //Assert
        }

        [Fact(DisplayName = "Handle should call PublishWorkPatternEventAsync on ApplicationUserRepository")]
        public async Task HandleShouldCallPublishWorkPatternEventAsyncOnApplicationUserRepository_WhenCommandIsSet()
        {
            // Arrange

            _unsServiceMock.Setup(x => x.PublishLocationAsync(It.IsAny<UsersEachLocation>(), It.IsAny<string>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new PublishLocationEvent(new UsersEachLocation("1", new List<int>{1,2,3}),"1");

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unsServiceMock
                .Verify(x => x.PublishLocationAsync(It.IsAny<UsersEachLocation>(), It.IsAny<string>(), CancellationToken.None), Times.Once);
        }
    }
}
