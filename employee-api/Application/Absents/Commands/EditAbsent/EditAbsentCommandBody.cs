using MediatR;
using employee_api.Models;
using System;

namespace employee_api.Application.ApplicationUsers.Queries.EditWorkPatterm
{
    public class EditAbsentCommandBody
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}