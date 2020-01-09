using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models
{
    public class reserveVenueModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? Duration { get; set; }
        public string TypeId { get; set; }
        [NotMapped]
        public SelectList Venues { get; set; }
    }
}
