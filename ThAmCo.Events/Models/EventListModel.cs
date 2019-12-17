using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models
{
    public class EventListModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }

        public Boolean Attended { get; set; }
        
    }
}
