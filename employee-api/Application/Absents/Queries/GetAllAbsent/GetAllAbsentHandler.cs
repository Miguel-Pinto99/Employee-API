using MediatR;
using employee_api.Data;
using employee_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using employee_api.Persistance;

namespace employee_api.Application.Absent.Queries.GetAllAbsent
{
    public class GetAllAbsentHandler : IRequestHandler<GetAllAbsentCommand, GetAllAbsentResponse>
    {
        private readonly IAbsentRepository _repository;

        public GetAllAbsentHandler(IAbsentRepository repository)
        {
            _repository = repository;

        }

        public async Task<GetAllAbsentResponse> Handle(GetAllAbsentCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var gotAbsent = await _repository.GetAllAbsentAsync(cancellationToken);

            return new GetAllAbsentResponse
            {
                ListAbsent = gotAbsent
            };
        }
    }
}
