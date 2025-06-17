using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.WorkPatterns.Commands.CreateWorkPattern;
using employee_api.Events;
using employee_api.Events.UnsEvents;
using employee_api.Events.UnsLogicEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.WorkPatterns.Commands.DeleteWorkPattern
{
    public class DeleteWorkPatternHandler : IRequestHandler<DeleteWorkPatternCommand, DeleteWorkPatternResponse>
    {
        private readonly IWorkPatternRepository _repository;
        private readonly IMediator _mediator;

        public DeleteWorkPatternHandler(IWorkPatternRepository repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

        }

        public async Task<DeleteWorkPatternResponse> Handle(DeleteWorkPatternCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var id = command.Id;
            var deletedWorkPattern = await _repository.DeleteWorkPatternAsync(id, cancellationToken);
            var eventPublishWorkPattern = new DeleteWorkPatternLogicEvent(deletedWorkPattern);
            await _mediator.Publish(eventPublishWorkPattern, cancellationToken);


            return new DeleteWorkPatternResponse
            {
                WorkPattern = deletedWorkPattern
            };
        }
    }
}
