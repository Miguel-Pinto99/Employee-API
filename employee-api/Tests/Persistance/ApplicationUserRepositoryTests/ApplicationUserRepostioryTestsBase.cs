using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Tests.Persistance.ApplicationUserRepositoryTests
{
    public class ApplicationUserRepositoryTestsBase : RepositoryTestsBase
    {
        protected readonly ApplicationUserRepository repository;

        public ApplicationUserRepositoryTestsBase()
        {
            repository = new ApplicationUserRepository(context);
            SetupDb(repository);
        }

        public async void SetupDb(ApplicationUserRepository repository)
        {
            var applicationUser = new ApplicationUser
            {
                Id = 1,
                FirstName = "Miguel",
                CheckedIn = true,
                OfficeLocation = "1",
                WorkPatterns = new List<WorkPattern>()
            };

            var userInDb = await repository.CreateApplicationUserAsync(applicationUser, CancellationToken.None);

        }

    }
}
