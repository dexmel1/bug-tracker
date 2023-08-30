using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BugTracker.Model
{
    public class Ticket
    {
        //TODO add validation, verify metrics with katrina

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int Priority { get; set; }
        public string Group { get; set; }

    }
}

[JsonSerializable(typeof(List<Ticket>))]
internal sealed partial class TicketContext : JsonSerializerContext
{

}
