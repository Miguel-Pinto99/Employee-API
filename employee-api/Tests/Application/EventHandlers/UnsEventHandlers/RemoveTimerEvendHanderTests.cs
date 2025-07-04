using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using employee_api.Application.EventHandlers.UnsEventHandlers;
using employee_api.Events.UnsEvents;
using employee_api.Models;
using employee_api.Timers;
using Xunit;
using employee_api.Application.Uns.UnsLogicEventHandlers;

namespace employee_api.Tests.Application.EventHandlers.UnsEventHandlers
{
    public class RemoveTimerEventHandlerEventHandler
    {
        private readonly Mock<ITimerService> _timerServiceMock;
        private readonly RemoveTimerEventHandler _handler;

        public RemoveTimerEventHandlerEventHandler()
        {
            _timerServiceMock = new Mock<ITimerService>(MockBehavior.Strict);
            _handler = new RemoveTimerEventHandler(_timerServiceMock.Object);
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

        [Fact(DisplayName = "Handle should call RemoveTimerEventAsync on TimerService")]
        public async Task HandleShouldCallRemoveTimerEventAsyncOnTimerService_WhenCommandIsSet()
        {
            // Arrange

            var listWorkPatternParts = new List<WorkPatternPart>
            {
                new WorkPatternPart
                {
                    StartTime = TimeSpan.FromHours(1),
                    EndTime  = TimeSpan.FromHours(3),
                    Day = 0
                },
            };

            var listWorkPatterns = new List<WorkPattern>
            {
                new WorkPattern
                {
                    UserId = 1,
                    Id = new Guid(),
                    StartDate = new DateTime(2022,1,1,0,0,0),
                    EndDate = new DateTime(2022,1,1,0,0,0),
                    Parts = listWorkPatternParts
                },
            };

            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = "1",
                WorkPatterns = listWorkPatterns
            };



            _timerServiceMock.Setup(x => x.RemoveTimersAsync(It.IsAny<ApplicationUser>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new RemoveTimerEvent(applicationUser);
            ;

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _timerServiceMock
                .Verify(x => x.RemoveTimersAsync(It.IsAny<ApplicationUser>(), CancellationToken.None), Times.Once);
        }
    }
}