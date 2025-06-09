using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Events;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.WorkPatterns.Commands.EditWorkPattern
{
    public class EditWorkPatternHandler : IRequestHandler<EditWorkPatternCommand, EditWorkPatternResponse>
    {
        private readonly IWorkPatternRepository _repository;
        private readonly IMediator _mediator;

        public EditWorkPatternHandler(IWorkPatternRepository repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

        }

        public async Task<EditWorkPatternResponse> Handle(EditWorkPatternCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var workPattern = new WorkPattern
            {
                Id = command.Id,
                StartDate = command.Body.StartDate,
                EndDate = command.Body.EndDate,
                Parts = command.Body.Parts
            };

            var updatedWP = await _repository.UpdateWorkPatternAsync(workPattern, cancellationToken);

            var commandGetApplicationUser = new GetApplicationUserCommand(updatedWP.UserId);
            var gotApplicationUser = await _mediator.Send(commandGetApplicationUser, cancellationToken);

            var eventPublishWorkPattern = new PublishWorkPatternEvent(gotApplicationUser.ApplicationUser);
            await _mediator.Publish(eventPublishWorkPattern, cancellationToken);


            return new EditWorkPatternResponse
            {
                WorkPattern = updatedWP
            };
        }
    }
}
