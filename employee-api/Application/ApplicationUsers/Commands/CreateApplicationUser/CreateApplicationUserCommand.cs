﻿using MediatR;

namespace employee_api.Application.ApplicationUsers.Commands.CreateApplicationUser
{
    public class CreateApplicationUserCommand : IRequest<CreateApplicationUserResponse>
    {
        public int Id { get; set; }
        public CreateApplicationUserCommandBody Body { get; set; }
        public CreateApplicationUserCommand(int id, CreateApplicationUserCommandBody body)
        {
            Id = id;
            Body = body;
        }
    }
}