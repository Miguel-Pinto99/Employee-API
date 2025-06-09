using MediatR;
using employee_api.Application.Absent.Commands.CreateAbsent;

namespace employee_api.Application.Absent.Commands.DeleteAbsent
{
    public class DeleteAbsentCommand : IRequest<DeleteAbsentResponse>
    {
        public Guid Id { get; set; }

        public DeleteAbsentCommand(Guid id)
        {
            Id = id;
        }

    }
}