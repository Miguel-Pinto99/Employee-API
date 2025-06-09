using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.Absent.Queries.GetAbsent
{
    public class GetAbsentHandler : IRequestHandler<GetAbsentCommand, GetAbsentResponse>
    {
        private readonly IAbsentRepository _repository;

        public GetAbsentHandler(IAbsentRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAbsentResponse> Handle(GetAbsentCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var id = command.Id;
            var gotAbsent = await _repository.GetAbsentAsync(id, cancellationToken);

            return new GetAbsentResponse
            {
                Absent = gotAbsent
            };
        }
    }
}
