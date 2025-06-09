using MediatR;
using employee_api.Models;
using System.Text.Json.Serialization;

namespace employee_api.Application.Absent.Commands.CreateAbsent
{

    public class CreateAbsentCommand : IRequest<CreateAbsentResponse>
        {

            [JsonIgnore]
            public Models.ApplicationUser? User { get; set; }
            public int UserId { get; set; }
            public CreateAbsentCommandBody Body { get; set; }
            public CreateAbsentCommand(int id, CreateAbsentCommandBody body)
            {
                UserId = id;
                Body = body;
            }

        }
}