﻿using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.CheckOutApplicationUser
{
    public class CheckOutApplicationUserCommand : IRequest<CheckOutApplicationUserResponse>
    {
        public int Id { get; set; }
        public CheckOutApplicationUserCommand(int id)
        {
            Id = id;
        }
    }
}