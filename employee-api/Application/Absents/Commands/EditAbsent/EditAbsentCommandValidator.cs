using FluentValidation;
using employee_api.Application.Absents.Commands.EditAbsent;

namespace employee_api.Application.Absent.Commands.EditAbsent
{
    public class EditAbsentCommandValidator : AbstractValidator<EditAbsentCommand>
    {
        public EditAbsentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Body).SetValidator(new EditAbsentCommandBodyValidator());
        }
    }
}