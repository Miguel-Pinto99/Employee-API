using FluentValidation;
using employee_api.Application.Absent.Commands.CreateAbsent;
using employee_api.Application.ApplicationUser.Commands.CreateApplicationUser;

namespace employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser
{
    public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
    {
        public CreateApplicationUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Body).SetValidator(new CreateApplicationUserCommandBodyValidator());
        }
    }
}