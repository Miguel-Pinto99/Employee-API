using FluentValidation;
using employee_api.Application.ApplicationUsers.Commands.EditApplicationUser;

namespace employee_api.Application.ApplicationUsers.Queries.EditApplicationUser
{
    public class EditApplicationUserCommandValidator : AbstractValidator<EditApplicationUserCommand>
    {
        public EditApplicationUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Body).SetValidator(new EditApplicationUserCommandBodyValidator());
        }
    }
}