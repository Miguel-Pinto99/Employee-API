using FluentValidation;
using employee_api.Application.Absent.Commands.DeleteAbsent;

namespace employee_api.Application.Absent.Commands.DeleteAbsent
{
    public class DeleteAbsentCommandValidator : AbstractValidator<DeleteAbsentCommand>
    {
        public DeleteAbsentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}