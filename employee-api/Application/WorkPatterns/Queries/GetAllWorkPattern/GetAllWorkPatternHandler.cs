using MediatR;
using employee_api.Infrastructure;
using employee_api.Models;
using employee_api.Persistance;

namespace employee_api.Application.WorkPatterns.Queries.GetAllWorkPattern
{
    public class GetAllWorkPatternHandler : IRequestHandler<GetAllWorkPatternCommand, GetAllWorkPatternResponse>
    {
        private readonly IWorkPatternRepository _repository;

        public GetAllWorkPatternHandler(IWorkPatternRepository repository)
        {
            _repository = repository;

        }

        public async Task<GetAllWorkPatternResponse> Handle(GetAllWorkPatternCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var gotWP = await _repository.GetAllWorkPatternsAsync(cancellationToken);

            return new GetAllWorkPatternResponse
            {
                listWorkPattern = gotWP
            };
        }
    }
}
