using MediatR;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Queries.CheckInApplicationUser
{
    public class CheckInApplicationUserHandler : IRequestHandler<CheckInApplicationUserCommand, CheckInApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;
        private readonly IMediator _mediator;

        public CheckInApplicationUserHandler(IApplicationUsersRepository repository, 
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

        }

        public async Task<CheckInApplicationUserResponse> Handle(CheckInApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            int id = command.Id;
            var checkInUser = await _repository.CheckInApplicationUserAsync(id, cancellationToken);
            var eventSendCheckIn = new PublishCheckInEvent(checkInUser);
            await _mediator.Publish(eventSendCheckIn, cancellationToken);

            return new CheckInApplicationUserResponse
            {
                ApplicationUser = checkInUser
            };
        }
    }
}
