using MediatR;

namespace employee_api.Application.ApplicationUsers.Queries.GetLocation
{
    public class GetLocationCommand : IRequest<GetLocationResponse>
    {
        public int OfficeLocation { get; set; }
        public GetLocationCommand(int officeLocation)
        {
            OfficeLocation = officeLocation;
        }
    }
}