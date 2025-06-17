using MediatR;

namespace employee_api.Application.Absent.Queries.GetAbsent
{
    public class GetAbsentCommand : IRequest<GetAbsentResponse>
    {
        public Guid Id { get; set; }
        public GetAbsentCommand(Guid id)
        {
            Id = id;
        }
    }
}