using MediatR;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Queries.GetAllLocation
{
    public class GetAllLocationHandler : IRequestHandler<GetAllLocationCommand, GetAllLocationResponse>
    {
        private readonly IApplicationUsersRepository _repository;

        public GetAllLocationHandler(IApplicationUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAllLocationResponse> Handle(GetAllLocationCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var listUsersEachLocation = await _repository.GetAllLocationsAsync(cancellationToken);

            return new GetAllLocationResponse
            {
                ListUserEachLocation = listUsersEachLocation
            };
        }
    }
}
