using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.EditApplicationUser
{
    public class EditApplicationUserCommand : IRequest<EditApplicationUserResponse>
    {
        public int Id { get; set; }
        public EditApplicationUserCommandBody Body { get; set; }
        public EditApplicationUserCommand(int id, EditApplicationUserCommandBody body)
        {
            Id = id;
            Body = body;
        }
    }
}