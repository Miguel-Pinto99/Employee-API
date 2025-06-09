using FluentValidation;

namespace employee_api.Application.ApplicationUsers.Queries.GetAllLocation
{
    public class GetAllLocationCommandValidator : AbstractValidator<GetAllLocationCommand>
    {
        public GetAllLocationCommandValidator()
        {
        }
    }
}