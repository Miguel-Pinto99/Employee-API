using MediatR;

namespace employee_api.Application.Absent.Queries.GetAllAbsent
{
    public class GetAllAbsentCommand : IRequest<GetAllAbsentResponse>
    {
    }
}