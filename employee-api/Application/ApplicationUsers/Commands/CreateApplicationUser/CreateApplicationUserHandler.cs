using MediatR;
using employee_api.Persistance;
using employee_api.Events.UnsLogicEvents;

namespace employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser
{
    public class CreateApplicationUserHandler : IRequestHandler<CreateApplicationUserCommand, CreateApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;
        private readonly IMediator _mediator;

        public CreateApplicationUserHandler(IApplicationUsersRepository repository, 
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<CreateApplicationUserResponse> Handle(CreateApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var user = new Models.ApplicationUser
            {
                Id = command.Id,
                FirstName = command.Body.FirstName,
                OfficeLocation = command.Body.OfficeLocation,
                Email = command.Body.Email,
                EmployeeNumber = command.Body.EmployeeNumber,
                SAMAcountName = command.Body.SamAccountName
            };

            var createdUser = await _repository.CreateApplicationUserAsync(user, cancellationToken);

            var eventUnsLogic = new CreateApplicationUserLogicEvent(createdUser);
            await _mediator.Publish(eventUnsLogic, cancellationToken);

            return new CreateApplicationUserResponse
            {
                ApplicationUser = createdUser
            };
        }
    }
}
