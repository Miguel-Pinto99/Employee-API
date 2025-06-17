using MediatR;
using employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm;

namespace employee_api.Application.WorkPatterns.Commands.EditWorkPattern
{
    public class EditWorkPatternCommand : IRequest<EditWorkPatternResponse>
    {
        public Guid Id { get; set; }
        public EditWorkPatternCommandBody Body { get; set; }
        public EditWorkPatternCommand(Guid id, EditWorkPatternCommandBody body)
        {
            Id = id;
            Body = body;
        }
    }
}
