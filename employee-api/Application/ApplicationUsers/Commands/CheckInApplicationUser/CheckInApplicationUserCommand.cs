using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.CheckInApplicationUser
{
    public class CheckInApplicationUserCommand : IRequest<CheckInApplicationUserResponse>
    {
        public int Id { get; set; }

        public CheckInApplicationUserCommand(int id)
        {
            Id = id;
        }
    }
}