using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.GetLocation
{
    public class GetLocationCommand : IRequest<GetLocationResponse>
    {
        public string OfficeLocation { get; set; }
        public GetLocationCommand(string officeLocation)
        {
            OfficeLocation = officeLocation;
        }
    }
}