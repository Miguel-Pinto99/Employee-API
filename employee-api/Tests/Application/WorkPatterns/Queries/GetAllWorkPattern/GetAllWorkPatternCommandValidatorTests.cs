using FluentValidation.TestHelper;
using Xunit;
using employee_api.Application.WorkPatterns.Queries.GetAllWorkPattern;

namespace employee_api.Tests.Application.WorkPatterns.Queries.GetAllWorkPattern
{
    public class GetAllWorkPatternCommandValidatorTests
    {
        private GetAllWorkPatternCommandValidator _validator;

        public GetAllWorkPatternCommandValidatorTests()
        {
            _validator = new GetAllWorkPatternCommandValidator();
        }

        [Fact(DisplayName = "when create a command should not have error")]
        public void Command_WhenCreated_ShouldNotHaveError()
        {
            var command = new GetAllWorkPatternCommand();
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(command => command);
        }
    }
}

