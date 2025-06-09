using MediatR;
using employee_api.Data;
using employee_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Queries.GetAllApplicationUser
{
    public class GetAllApplicationUserHandler : IRequestHandler<GetAllApplicationUserCommand, GetAllApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;

        public GetAllApplicationUserHandler(IApplicationUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAllApplicationUserResponse> Handle(GetAllApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var gotUsers = await _repository.GetAllApplicationUserAsync(cancellationToken);

            return new GetAllApplicationUserResponse
            {
                listApplicationUser = gotUsers
            };
        }
    }
}
