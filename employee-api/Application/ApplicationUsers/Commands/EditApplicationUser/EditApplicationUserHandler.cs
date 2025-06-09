using MediatR;
using employee_api.Application.ApplicationUsers.Queries.EditApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.ApplicationUsers.Queries.GetLocation;
using employee_api.Models;
using employee_api.Persistance;
using employee_api.Events;
using employee_api.Application.ApplicationUsers.Commands.DeleteApplicationUser;
using employee_api.Events.UnsEvents;
using employee_api.Events.UnsLogicEvents;

namespace employee_api.Application.ApplicationUsers.Commands.EditApplicationUser
{
    public class EditApplicationUserHandler : IRequestHandler<EditApplicationUserCommand, EditApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;
        private readonly IMediator _mediator;

        public EditApplicationUserHandler(IApplicationUsersRepository repository, 
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<EditApplicationUserResponse> Handle(EditApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var user = new Models.ApplicationUser
            {
                Id = command.Id,
                FirstName = command.Body.FirstName,
                OfficeLocation = command.Body.OfficeLocation
            };

            //False: Track reference
            var oldUser = await _repository.GetApplicationUserAsync(user.Id, false, cancellationToken);
            var newUser = await _repository.UpdateApplicationUserAsync(user, cancellationToken);

            var eventUnsLogic = new EditApplicationUserLogicEvent(newUser,oldUser);
            await _mediator.Publish(eventUnsLogic, cancellationToken);

            return new EditApplicationUserResponse
            {
                ApplicationUser = newUser
            };
        }
    }
}
