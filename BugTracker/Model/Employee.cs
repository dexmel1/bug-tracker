using SQLite;
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

        [PrimaryKey, AutoIncrement]
        public int Id { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public string Role { get; set; }

    }
}

