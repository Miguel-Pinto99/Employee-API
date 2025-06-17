using FluentValidation;

namespace employee_api.Application.ApplicationUsers.Queries.GetAllApplicationUser
{
    public class GetAllApplicationUserCommandValidator : AbstractValidator<GetAllApplicationUserCommand>
    {
        public GetAllApplicationUserCommandValidator()
        {
        }
    }
}