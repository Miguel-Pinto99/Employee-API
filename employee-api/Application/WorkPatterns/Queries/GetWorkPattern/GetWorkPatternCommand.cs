using MediatR;

namespace employee_api.Application.WorkPatterns.Queries.GetWorkPattern
{
    public class GetWorkPatternCommand : IRequest<GetWorkPatternResponse>
    {
        public Guid Id { get; set; }
        public GetWorkPatternCommand(Guid id)
        {
            Id = id;
        }
    }
}