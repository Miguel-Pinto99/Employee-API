using FluentValidation;
using employee_api.Application.WorkPatterns.Commands.CreateWorkPattern;

namespace employee_api.Application.WorkPatterns.Commands.EditWorkPattern
{
    public class EditWorkPatternCommandValidator : AbstractValidator<EditWorkPatternCommand>
    {
        public EditWorkPatternCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Body).SetValidator(new EditWorkPatternCommandBodyValidator());
        }
    }
}