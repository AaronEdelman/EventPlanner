using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class Entertainment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Restriction { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

    }
}