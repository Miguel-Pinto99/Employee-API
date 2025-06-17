using FluentValidation;

namespace employee_api.Application.WorkPatterns.Queries.GetWorkPattern
{
    public class GetWorkPatternCommandValidator : AbstractValidator<GetWorkPatternCommand>
    {
        public GetWorkPatternCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}