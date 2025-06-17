using MediatR;

namespace employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser
{
    public class CreateApplicationUserCommandBody
    {
        public string FirstName { get; set; }
        public string OfficeLocation { get; set; }

        public string Email { get; set; }
        public string EmployeeNumber { get; set; }
        public string SamAccountName { get; set; }
    }
}