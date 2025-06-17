using FluentValidation;

namespace employee_api.Application.Absent.Queries.GetAllAbsent
{
    public class GetAllAbsentCommandValidator : AbstractValidator<GetAllAbsentCommand>
    {
        public GetAllAbsentCommandValidator()
        {
        }
    }
}