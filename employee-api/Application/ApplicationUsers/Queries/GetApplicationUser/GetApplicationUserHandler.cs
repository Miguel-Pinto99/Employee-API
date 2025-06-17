using MediatR;
using employee_api.Data;
using employee_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using employee_api.Persistance;

namespace employee_api.Application.ApplicationUsers.Queries.GetApplicationUser
{
    public class GetApplicationUserHandler : IRequestHandler<GetApplicationUserCommand, GetApplicationUserResponse>
    {
        private readonly IApplicationUsersRepository _repository;

        public GetApplicationUserHandler(IApplicationUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetApplicationUserResponse> Handle(GetApplicationUserCommand command, CancellationToken cancellationToken)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            int id = command.Id;
            var getUser = await _repository.GetApplicationUserAsync(id, cancellationToken);

            return new GetApplicationUserResponse
            {
                ApplicationUser = getUser
            };
        }
    }
}
