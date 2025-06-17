using MediatR;
using employee_api.Data;
using employee_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Application.Absent.Commands.EditAbsent;
using employee_api.Events;
using employee_api.Events.UnsEvents;
using employee_api.Infrastructure;
using employee_api.Persistance;
using employee_api.Events.AbsentLogicEvents;

namespace employee_api.Application.Absent.Commands.EditAbsent
{
    public class EditAbsentHandler : IRequestHandler<EditAbsentCommand, EditAbsentResponse>
    {
        private readonly IAbsentRepository _repository;
        private readonly IMediator _mediator;

        public EditAbsentHandler(IAbsentRepository repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<EditAbsentResponse> Handle(EditAbsentCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var absent = new Models.Absent
            {
                Id = command.Id,
                StartDate = command.Body.StartDate,
                EndDate = command.Body.EndDate,
                Description = command.Body?.Description,
            };

            var updatedAbsent = await _repository.UpdateAbsentAsync(absent, cancellationToken);
            var eventPublishAbsent = new AbsentLogicEvent(updatedAbsent);
            await _mediator.Publish(eventPublishAbsent, cancellationToken);


            return new EditAbsentResponse
            {
                Absent = updatedAbsent
            };
        }
    }
}
