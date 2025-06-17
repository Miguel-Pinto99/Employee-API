namespace employee_api.Models
{
    public class UsersEachLocation
    {
        public string OfficeLocation { get; set; }
        public List<int> UserIds { get; set; }

        public UsersEachLocation(string officeLocation, List<int> userIds)
        {
            OfficeLocation = officeLocation;
            UserIds = userIds;
        }
    }
}
