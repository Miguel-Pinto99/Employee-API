using FluentValidation;

namespace employee_api.Application.ApplicationUsers.Queries.GetLocation
{
    public class GetLocationCommandValidator : AbstractValidator<GetLocationCommand>
    {
        public GetLocationCommandValidator()
        {
            RuleFor(x => x.OfficeLocation).NotEmpty();
        }
    }
}