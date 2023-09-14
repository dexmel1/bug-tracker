using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Model
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Lead { get; set; }
        public DateTime Created { get;} = DateTime.Now;
        public DateTime Updated { get;}
        public DateTime EstCompletion { get; set; }

    }
}
