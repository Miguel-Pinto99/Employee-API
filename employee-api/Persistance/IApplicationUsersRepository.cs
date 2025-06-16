using Microsoft.AspNetCore.Mvc;
using employee_api.Models;
using Microsoft.AspNetCore.Http;
using employee_api.Data;
using employee_api.Persistance;

namespace employee_api.Persistance
{
    public interface IApplicationUsersRepository
    {
        public Task<ApplicationUser> CreateApplicationUserAsync(ApplicationUser createdUser, CancellationToken cancellationToken);
        public Task<ApplicationUser> GetApplicationUserAsync(int id, CancellationToken cancellationToken);
        public Task<ApplicationUser> GetApplicationUserAsync(int id, bool track, CancellationToken cancellationToken);
        public Task<ApplicationUser> UpdateApplicationUserAsync(ApplicationUser updatedUser, CancellationToken cancellationToken);
        public Task<ApplicationUser> DeleteApplicationUserAsync(int applicationUser, CancellationToken cancellationToken);


        public Task<ApplicationUser> CheckInApplicationUserAsync(int applicationUser, CancellationToken cancellationToken);
        public Task<ApplicationUser> CheckOutApplicationUserAsync(int applicationUser, CancellationToken cancellationToken);

        public Task<UsersEachLocation> GetLocationAsync(string officeLocation, CancellationToken cancellationToken);

        public Task<List<ApplicationUser>> GetAllApplicationUserAsync(CancellationToken cancellationToken);
        
        public Task<List<UsersEachLocation>> GetAllLocationsAsync(CancellationToken cancellationToken);
        
    }
}
