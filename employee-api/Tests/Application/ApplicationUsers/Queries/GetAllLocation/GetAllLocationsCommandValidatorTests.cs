using FluentValidation.TestHelper;
using employee_api.Application.ApplicationUsers.Queries.GetAllLocation;
using employee_api.Application.WorkPatterns.Queries.GetAllWorkPattern;
using Xunit;

namespace PVSDashboard.Tests.Application.ApplicationUsers.Queries.GetAllLocation
{
    public class GetAllLocationsCommandValidatorTests
    {
        private GetAllLocationCommandValidator _validator;

        public GetAllLocationsCommandValidatorTests()
        {
            _validator = new GetAllLocationCommandValidator();
        }

        [Fact(DisplayName = "when create a command should not have error")]
        public void Command_WhenCreated_ShouldNotHaveError()
        {
            var command = new GetAllLocationCommand();
            _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(command => command);
        }
    }
}

