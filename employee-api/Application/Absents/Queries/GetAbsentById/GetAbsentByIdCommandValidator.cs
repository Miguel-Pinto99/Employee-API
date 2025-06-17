using FluentValidation;

namespace employee_api.Application.Absent.Queries.GetAbsentByIdById
{
    public class GetAbsentByIdCommandValidator : AbstractValidator<GetAbsentByIdCommand>
    {
        public GetAbsentByIdCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}