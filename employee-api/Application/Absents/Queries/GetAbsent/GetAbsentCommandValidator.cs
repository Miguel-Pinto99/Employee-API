using FluentValidation;

namespace employee_api.Application.Absent.Queries.GetAbsent
{
    public class GetAbsentCommandValidator : AbstractValidator<GetAbsentCommand>
    {
        public GetAbsentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}