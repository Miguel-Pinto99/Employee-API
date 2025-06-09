using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.EditApplicationUser
{
    public class EditApplicationUserCommandBody
    {
        public string FirstName { get; set; }
        public int OfficeLocation { get; set; }
    }
}