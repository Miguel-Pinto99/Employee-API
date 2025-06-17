using FluentValidation;

namespace employee_api.Application.WorkPatterns.Commands.DeleteWorkPattern
{
    public class DeleteWorkPatternCommandValidator : AbstractValidator<DeleteWorkPatternCommand>
    {
        public DeleteWorkPatternCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}