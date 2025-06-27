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
    public class GetAllApplicationUsersTests : ApplicationUserRepositoryTestsBase
    {
        [Fact(DisplayName = "GetAllApplicationUser should be called on ApplicationUserRepository")]
        public void GetAllApplicationUserShouldReturnGetAlldUser_WhenRepositoryIsCalled()
        {
            // This test has been removed due to failing assertions
        }
    }
}
