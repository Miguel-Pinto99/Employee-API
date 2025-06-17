using FluentValidation;

namespace employee_api.Application.Absent.Commands.CreateAbsent
{
    public class CreateAbsentCommandValidator : AbstractValidator<CreateAbsentCommand>
    {
        public CreateAbsentCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Body).SetValidator(new CreateAbsentCommandBodyValidator());
        }
    }
}