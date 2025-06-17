using MediatR;

namespace employee_api.Application.ApplicationUsers.Commands.DeleteApplicationUser
{
    public class DeleteApplicationUserCommand : IRequest<DeleteApplicationUserResponse>
    {
        public int Id { get; set; }

        public DeleteApplicationUserCommand(int id)
        {
            Id = id;
        }
    }
}