using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Data
{
    public class Event
    {   
        public Event()
        {
            if(this.Staffing == null)
            {
                this.Staffing = new List<Staff>();
            }
            else
            {
                this.Staffing = this.Staffing;
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        [Required, MaxLength(3), MinLength(3)]
        public string TypeId { get; set; }

        public List<GuestBooking> Bookings { get; set; }

        public List<Staff> Staffing { get; set; }
    }
}
