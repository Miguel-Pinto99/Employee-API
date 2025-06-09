using FluentValidation;

namespace employee_api.Application.ApplicationUsers.Queries.CheckInApplicationUser
{
    public class CheckInApplicationUserCommandValidator : AbstractValidator<CheckInApplicationUserCommand>
    {
        public CheckInApplicationUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}