using employee_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace employee_api.Models
{
    public class ApplicationUser 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string OfficeLocation { get; set; }
        public bool CheckedIn { get; set; }
        public string Email { get; set; }
        public string SAMAcountName { get; set; }
        public string EmployeeNumber { get; set; }
        public List<WorkPattern> WorkPatterns { get; set; }
    }
}



