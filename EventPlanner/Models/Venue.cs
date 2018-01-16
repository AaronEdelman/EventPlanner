using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Venue")]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Display(Name = "Disabled Friendly")]
        public bool IsDisabledFriendly { get; set; }
        [Display(Name = "Outdoors")]
        public bool IsOutdoors { get; set; }
        [Display(Name = "Has Seating")]
        public bool HasSeating { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
}