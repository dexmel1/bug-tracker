using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BugTracker.Model
{
    public class Employee
    {
        //TODO: add validation

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

    }
}

[JsonSerializable(typeof(List<Employee>))]
internal sealed partial class EmployeeContext : JsonSerializerContext
{

}
