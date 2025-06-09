using MediatR;
using employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm;

namespace employee_api.Application.Absent.Commands.EditAbsent
{
    public class EditAbsentCommand : IRequest<EditAbsentResponse>
    {
        public Guid Id { get; set; }
        public EditAbsentCommandBody Body { get; set; }
        public EditAbsentCommand(Guid id, EditAbsentCommandBody body)
        {
            Id = id;
            Body = body;
        }
    }
}
