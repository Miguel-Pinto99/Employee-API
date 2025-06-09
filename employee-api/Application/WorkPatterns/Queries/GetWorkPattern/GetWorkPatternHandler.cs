using MediatR;
using employee_api.Application.ApplicationUsers.Queries.GetApplicationUser;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.WorkPatterns.Queries.GetWorkPattern
{
    public class GetWorkPatternHandler : IRequestHandler<GetWorkPatternCommand, GetWorkPatternResponse>
    {
        private readonly IWorkPatternRepository _repository;

        public GetWorkPatternHandler(IWorkPatternRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetWorkPatternResponse> Handle(GetWorkPatternCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var id = command.Id;
            var gotWP = await _repository.GetWorkPatternAsync(id, cancellationToken);

            return new GetWorkPatternResponse
            {
                WorkPattern = gotWP
            };
        }
    }
}
