using FluentValidation.TestHelper;
using Xunit;
using employee_api.Application.Absent.Queries.GetAllAbsent;

namespace employee_api.Tests.Application.Absents.Queries.GetAllAbsent
{
    public class GetAllAbsentCommandValidatorTests
    {
        private GetAllAbsentCommandValidator _validator;

        public GetAllAbsentCommandValidatorTests()
        {
            _validator = new GetAllAbsentCommandValidator();
        }

        [Fact(DisplayName = "when create a command should not have error")]
        public void UserId_WhenTwo_ShouldNotHaveError()
        {
            var command = new GetAllAbsentCommand();
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(command => command);
        }
    }
}

