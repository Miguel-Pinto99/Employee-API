using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.Absent.Commands.CreateAbsent;
using employee_api.Application.Absent.Commands.DeleteAbsent;
using employee_api.Events;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;
using employee_api.Events.AbsentLogicEvents;

namespace employee_api.Application.Absent.Commands.DeleteAbsent
{
    public class DeleteAbsentHandler : IRequestHandler<DeleteAbsentCommand, DeleteAbsentResponse>
    {
        private readonly IAbsentRepository _repository;
        private readonly IMediator _mediator;

        public DeleteAbsentHandler(IAbsentRepository repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

        }

        public async Task<DeleteAbsentResponse> Handle(DeleteAbsentCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var id = command.Id;
            var deletedAbsent = await _repository.DeleteAbsentAsync(id, cancellationToken);

            var eventPublishAbsent = new AbsentLogicEvent(deletedAbsent);
            await _mediator.Publish(eventPublishAbsent, cancellationToken);


            return new DeleteAbsentResponse
            {
                Absent = deletedAbsent
            };
        }
    }
}
