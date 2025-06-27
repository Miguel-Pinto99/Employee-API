using FluentValidation.TestHelper;
using employee_api.Application.WorkPatterns.Commands.DeleteWorkPattern;
using Xunit;

namespace employee_api.Tests.Application.WorkPatterns.Commands.DeleteWorkPattern
{
    public class DeleteWorkPatternCommandValidatorTests
    {
        private DeleteWorkPatternCommandValidator _validator;

        public DeleteWorkPatternCommandValidatorTests()
        {
            _validator = new DeleteWorkPatternCommandValidator();
        }

        [Fact(DisplayName = "UserId when zero should have error")]
        public void UserId_WhenZero_ShouldHaveError()
        {
            var command = new DeleteWorkPatternCommand(new Guid());
            _validator.TestValidate(command).ShouldHaveValidationErrorFor(command => command.Id);
        }


    }
}

