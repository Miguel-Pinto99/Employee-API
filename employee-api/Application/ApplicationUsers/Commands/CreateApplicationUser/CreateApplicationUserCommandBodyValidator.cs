using FluentValidation;
using employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser;

namespace employee_api.Application.ApplicationUser.Commands.CreateApplicationUser;

public class CreateApplicationUserCommandBodyValidator : AbstractValidator<CreateApplicationUserCommandBody>
{
    public CreateApplicationUserCommandBodyValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.OfficeLocation).GreaterThan(0);
    }
}
