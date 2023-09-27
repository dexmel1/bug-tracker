using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BugTracker.Model
{
    public class Ticket
    {
        //TODO add validation, verify metrics with katrina

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; } = null;
        public DateTime? Updated { get; set; } = null;
        public int Priority { get; set; }
        public string Status { get; set; }
        public bool IsClosed { get; set; } = false;

        [ForeignKey(nameof(Employee.Id))]
        public int AssignedTo { get; set; }

        [ForeignKey(nameof(Project.Id))]
        public int ProjectAssign { get; set; }

    }
}

