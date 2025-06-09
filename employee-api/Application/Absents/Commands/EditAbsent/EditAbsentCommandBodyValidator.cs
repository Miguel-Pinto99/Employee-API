using FluentValidation;
using employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm;

namespace employee_api.Application.Absents.Commands.EditAbsent
{
    public class EditAbsentCommandBodyValidator : AbstractValidator<EditAbsentCommandBody>
    {
        public EditAbsentCommandBodyValidator()
        {

            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        }
    }
}
