using FluentValidation;
using employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm;
using employee_api.Models;

namespace employee_api.Application.WorkPatterns.Commands.EditWorkPattern;

public class EditWorkPatternCommandBodyValidator : AbstractValidator<EditWorkPatternCommandBody>
{
    public EditWorkPatternCommandBodyValidator()
    {

        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        RuleFor(x => x.Parts).NotEmpty();
        RuleForEach(x => x.Parts)
            .SetValidator(new WorkPatternPartValidator());
    }
}
public class WorkPatternPartValidator : AbstractValidator<WorkPatternPart>
{
    public WorkPatternPartValidator()
    {
        RuleFor(x => x.StartTime).GreaterThanOrEqualTo(TimeSpan.Zero);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        RuleFor(x => x.EndTime).LessThan(TimeSpan.FromDays(1));
        RuleFor(x => x.Day).NotEmpty();
    }
}
