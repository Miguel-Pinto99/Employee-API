using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using employee_api.Persistance;
using Xunit;
using employee_api.Application.WorkPatterns.Queries.GetWorkPattern;
using MediatR;

namespace employee_api.Tests.Application.WorkPatterns.Queries.GetWorkPattern
{
    public class GetWorkPatternHandlerTests
    {
        private readonly Mock<IWorkPatternRepository> _workPatternRepositoryMock;
        private readonly GetWorkPatternHandler _handler;

        public GetWorkPatternHandlerTests()
        {
            _workPatternRepositoryMock = new Mock<IWorkPatternRepository>(MockBehavior.Strict);
            _handler = new GetWorkPatternHandler(_workPatternRepositoryMock.Object);
        }

        [Fact(DisplayName = "Handle should throw ArgumentNullException when command is not set")]
        public async Task HandleShouldThrowArgumentNullException_WhenCommandIsNotSet()
        {
            // Arrange

            Func<Task<GetWorkPatternResponse>> act = async () => await _handler.Handle(null, CancellationToken.None);
            ExceptionAssertions<ArgumentNullException> exception = await act.Should().ThrowAsync<ArgumentNullException>();

            exception.WithMessage("Value cannot be null. (Parameter 'command')");
            // Act

            // Assert
        }

        [Fact(DisplayName = "Handle should call GetWorkPatternAsync on WorkPatternRepository")]
        public async Task HandleShouldCallGetWorkPatternAsyncOnWorkPatternRepository_WhenCommandIsSet()
        {
            // Arrange
            var workPattern = new employee_api.Models.WorkPattern
            {
                Id = Guid.NewGuid(),
                UserId = 1,
                StartDate = new DateTime(2022, 11, 29, 10, 0, 0),
                EndDate = new DateTime(2022, 11, 30, 0, 0, 0)
            };

            _workPatternRepositoryMock
                .Setup(x => x.GetWorkPatternAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync(workPattern);

            var command = new GetWorkPatternCommand(new Guid());

            // Act
            GetWorkPatternResponse response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.WorkPattern.Should().NotBeNull();

            _workPatternRepositoryMock
                .Verify(x => x.GetWorkPatternAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once);
        }
    }
}
