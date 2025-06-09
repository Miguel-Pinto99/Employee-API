using MediatR;
using employee_api.Models;
using System.Text.Json.Serialization;

namespace employee_api.Application.WorkPatterns.Commands.CreateWorkPattern
{

    public class CreateWorkPatternCommand : IRequest<CreateWorkPatternResponse>
        {

            [JsonIgnore]
            public Models.ApplicationUser? User { get; set; }
            public int UserId { get; set; }
            public CreateWorkPatternCommandBody Body { get; set; }
            public CreateWorkPatternCommand(int id, CreateWorkPatternCommandBody body)
            {
                UserId = id;
                Body = body;
            }

        }
}