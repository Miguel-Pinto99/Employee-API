using MediatR;
using employee_api.Application.WorkPatterns.Commands.CreateWorkPattern;

namespace employee_api.Application.WorkPatterns.Commands.DeleteWorkPattern
{
    public class DeleteWorkPatternCommand : IRequest<DeleteWorkPatternResponse>
    {
        public Guid Id { get; set; }

        public DeleteWorkPatternCommand(Guid id)
        {
            Id = id;
        }
    }
}