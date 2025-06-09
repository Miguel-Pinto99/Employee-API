using FluentAssertions.Specialized;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;
using employee_api.Events.AbsentLogicEvents;
using employee_api.Infrastructure;
using employee_api.Timers;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.Absent.Queries.GetAllAbsent;
using employee_api.Events.UnsEvents;
using employee_api.Application.EventHandlers.AbsentLogic;
using employee_api.Models;

namespace PVSDashboard.Tests.Application.EventHandlers.AbsentLogicEventHandlers
{
    public class AbsentLogicEventHandlerTests
    {
        private readonly Mock<IUnsService> _unsServiceMock;
        private readonly Mock<ITimerService> _timerServiceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AbsentLogicEventHandler _handler;

        public AbsentLogicEventHandlerTests()
        {
            _unsServiceMock = new Mock<IUnsService>(MockBehavior.Strict);
            _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            _timerServiceMock = new Mock<ITimerService>(MockBehavior.Strict);
            _handler = new AbsentLogicEventHandler(_unsServiceMock.Object, _mediatorMock.Object, _timerServiceMock.Object);
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

        [Fact(DisplayName = "Handle should call AbsentLogicEventAsync on ApplicationUserRepository")]
        public async Task HandleShouldCallAbsentLogicEventAsyncOnApplicationUserRepository_WhenCommandIsSet()
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
                OfficeLocation = 1,
                WorkPatterns = listWorkPatterns
            };

            var listAllAbsents = new List<employee_api.Models.Absent>
            {
                new employee_api.Models.Absent
                {
                    UserId= 1,
                    Id = new Guid(),
                    StartDate = DateTime.Now,

                }
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetApplicationUserCommand>(), CancellationToken.None))
                .ReturnsAsync(new GetApplicationUserResponse { ApplicationUser = applicationUser });

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllAbsentCommand>(), CancellationToken.None))
                .ReturnsAsync(new GetAllAbsentResponse { ListAbsent = listAllAbsents });

            _mediatorMock.Setup(x => x.Publish(It.IsAny<PublishWorkPatternEvent>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            _timerServiceMock.Setup(x => x.RemoveAbsentsAsync(It.IsAny<List<employee_api.Models.Absent>>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            _unsServiceMock.Setup(x => x.CheckTodayAbsentsAsync(It.IsAny<List<employee_api.Models.Absent>>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            _timerServiceMock.Setup(x => x.ReinitializeTimersAsync(It.IsAny<ApplicationUser>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var command = new AbsentLogicEvent(new employee_api.Models.Absent
            {
                UserId = 1,
                StartDate = new DateTime(2022, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2023, 1, 1, 0, 0, 0),
                Id = new Guid()
            });
            ;

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetApplicationUserCommand>(), CancellationToken.None), Times.Once);
            _unsServiceMock
                .Verify(x => x.CheckTodayAbsentsAsync(It.IsAny<List<employee_api.Models.Absent>>(), CancellationToken.None), Times.Once);
            _mediatorMock
                .Verify(x => x.Send(It.IsAny<GetAllAbsentCommand>(), CancellationToken.None), Times.Once);
            _mediatorMock
                .Verify(x => x.Publish(It.IsAny<PublishWorkPatternEvent>(), CancellationToken.None), Times.Once);
            _timerServiceMock
                .Verify(x => x.ReinitializeTimersAsync(It.IsAny<employee_api.Models.ApplicationUser>(), CancellationToken.None), Times.Once);
            _timerServiceMock
                .Verify(x => x.RemoveAbsentsAsync(It.IsAny<List<employee_api.Models.Absent>>(), CancellationToken.None), Times.Once);
        }
    }
}
