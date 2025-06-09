using FluentValidation;

namespace employee_api.Application.WorkPatterns.Queries.GetAllWorkPattern
{
    public class GetAllWorkPatternCommandValidator : AbstractValidator<GetAllWorkPatternCommand>
    {
        public GetAllWorkPatternCommandValidator()
        {
        }
    }
}