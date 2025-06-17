using MediatR;
using employee_api.Persistance;

namespace employee_api.Application.Absent.Queries.GetAbsentByIdById
{
    public class GetAbsentByIdHandler : IRequestHandler<GetAbsentByIdCommand, GetAbsentByIdResponse>
    {
        private readonly IAbsentRepository _repository;

        public GetAbsentByIdHandler(IAbsentRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAbsentByIdResponse> Handle(GetAbsentByIdCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var id = command.UserId;
            var gotListAbsent = await _repository.GetAbsentByIdAsync(id, cancellationToken);

            return new GetAbsentByIdResponse
            {
                ListAbsent = gotListAbsent
            };
        }
    }
}
