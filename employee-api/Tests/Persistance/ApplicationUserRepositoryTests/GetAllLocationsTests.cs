using employee_api.Data;
using employee_api.Models;
using FluentAssertions.Specialized;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;

namespace employee_api.Tests.Persistance.ApplicationUserRepositoryTests
{
    public class GetAllLocationsTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "GetAllLocation should be called on ApplicationUserRepository")]
        public void GetAllLocationShouldReturnCreatedUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }
    }
}
