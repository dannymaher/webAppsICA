using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models
{
    public class EventViewModel
    {
        public int id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String Surname { get; set; }

        [Required]
        public Boolean Attended { get; set; }

    }
}
