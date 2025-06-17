using MediatR;

namespace employee_api.Application.Absent.Queries.GetAbsentByIdById
{
    public class GetAbsentByIdCommand : IRequest<GetAbsentByIdResponse>
    {
        public int UserId { get; set; }

        public GetAbsentByIdCommand(int userId)
        {
            UserId = userId;
        }
    }
}