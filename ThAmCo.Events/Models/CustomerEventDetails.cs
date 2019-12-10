using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models
{
    public class CustomerEventDetails
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public IEnumerable<EventListModel> Events { get; set; }


    }
}
