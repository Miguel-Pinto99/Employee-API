using MediatR;
using employee_api.Data;
using employee_api.Models;
using Microsoft.EntityFrameworkCore;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Queries.GetLocation
{
    public class GetLocationHandler : IRequestHandler<GetLocationCommand, GetLocationResponse>
    {
        private readonly IApplicationUsersRepository _repository;

        public GetLocationHandler(IApplicationUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetLocationResponse> Handle(GetLocationCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            string officeLocation = command.OfficeLocation;
            UsersEachLocation usersEachLocation = await _repository.GetLocationAsync(officeLocation, cancellationToken);

            return new GetLocationResponse
            {
                UserEachLocation = usersEachLocation
            };
        }
    }
}
