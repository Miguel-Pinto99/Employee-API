using FluentValidation;
using employee_api.Application.ApplicationUsers.Queries.EditApplicationUser;

namespace employee_api.Application.ApplicationUsers.Commands.EditApplicationUser;

public class EditApplicationUserCommandBodyValidator : AbstractValidator<EditApplicationUserCommandBody>
{
    public EditApplicationUserCommandBodyValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.OfficeLocation).GreaterThan(0);
    }
}
