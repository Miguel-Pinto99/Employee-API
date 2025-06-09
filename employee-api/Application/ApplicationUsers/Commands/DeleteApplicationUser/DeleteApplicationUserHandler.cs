using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetLocation;
using employee_api.Events;
using employee_api.Events.UnsEvents;
using employee_api.Events.UnsLogicEvents;
using employee_api.Infrastructure;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Commands.DeleteApplicationUser
{
    public class DeleteApplicationUserHandler : IRequestHandler<DeleteApplicationUserCommand, DeleteApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;
        private readonly IMediator _mediator;

        public DeleteApplicationUserHandler(IApplicationUsersRepository repository, 
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<DeleteApplicationUserResponse> Handle(DeleteApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            int id = command.Id;

            var commandGetUser = new GetApplicationUserCommand(id);
            var responseGetUser = await _mediator.Send(commandGetUser, cancellationToken);            
            var deleteOficceLocation = responseGetUser.ApplicationUser.OfficeLocation;

            var deletedUser = await _repository.DeleteApplicationUserAsync(id, cancellationToken);

            var commandGetOfficeLocation = new GetLocationCommand(deleteOficceLocation);
            var responseGetOfficeLocation = await _mediator.Send(commandGetOfficeLocation, cancellationToken);
            var usersEachLocation = responseGetOfficeLocation.UserEachLocation;

            var eventUnsLogic = new DeleteApplicationUserLogicEvent(deletedUser,usersEachLocation);
            await _mediator.Publish(eventUnsLogic, cancellationToken);

            return new DeleteApplicationUserResponse
            {
                ApplicationUser = deletedUser
            };
        }
    }
}
