using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPlanner.Models
{
    public class Venue
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDisabledFriendly { get; set; }
        public bool IsOutdoors { get; set; }
        public bool HasSeating { get; set; }
    }
}